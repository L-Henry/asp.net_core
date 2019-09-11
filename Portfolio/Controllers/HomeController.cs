using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Portfolio.Data;
using Portfolio.Domain;
using Portfolio.Models;

namespace Portfolio.Controllers
{
    public class HomeController : Controller
    {
        private readonly ProjectContext _context;

        public HomeController(ProjectContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            List<ProjectListViewModel> projecten = new List<ProjectListViewModel>();
            IEnumerable<Project> projectenFromDatabase = _context.GetProjecten();

            foreach (var project in projectenFromDatabase)
            {
                projecten.Add(new ProjectListViewModel()
                {
                    Id = project.Id,
                    Titel = project.Titel,
                    Beschrijving = project.Beschrijving,
                    ImageFile = project.Image,
                    Status = project.Status.Naam,
                    Tags = project.TagProjects.Select(tagprojects => tagprojects.Tag).Select(tag => tag.Naam).ToList()
                    //Tags = _context.TagProject.Where(x => project.Id == x.Project.Id).Select(t => t.Tag.Naam.ToLower().Trim()).ToList(),

                });
            }

            ProjectFilterListViewModel vm = new ProjectFilterListViewModel
            {
                Projecten = projecten
            };

            return View(vm);
        }

        [HttpGet]
        public IActionResult Index(int statusId, string tags)
        {
            List<ProjectListViewModel> projecten = new List<ProjectListViewModel>();
            //IEnumerable<Project> projectenFromDatabase = _context.GetProjecten();
            string[] tagArray = new string[0];
            if (!string.IsNullOrEmpty(tags))
            {
                tagArray = tags.Split(" ");
            }

            IQueryable<Project> query = _context.Projecten.Include(x => x.Status)
                                                          .Include(x=>x.TagProjects).ThenInclude(x=>x.Tag);

            if (statusId == 0 && string.IsNullOrEmpty(tags))
            {
                ;
            }
            else if (statusId != 0 && string.IsNullOrEmpty(tags))
            {
                query = query.Where(x => x.Status.Id == statusId);
            }
            else if (!string.IsNullOrEmpty(tags) && statusId == 0)
            {
                query = query.Where(p => tagArray.All(x => p.TagProjects.Select(tagP => tagP.Tag).Select(tag => tag.Naam.ToLower()).Contains(x.ToLower())));
            }
            else
            {
                query = query.Where(p => p.Status.Id == statusId && tagArray.All(t => p.TagProjects.Select(tagP => tagP.Tag).Select(tag => tag.Naam.ToLower()).Contains(t.ToLower())));
            }

            var projectenFromQuery = query.ToList();
            foreach (var project in projectenFromQuery)
            {
                projecten.Add(new ProjectListViewModel
                {
                    Id = project.Id,
                    Titel = project.Titel,
                    Beschrijving = project.Beschrijving,
                    ImageFile = project.Image,
                    Status = project.Status.Naam,
                    Tags = project.TagProjects.Select(tagprojects => tagprojects.Tag).Select(tag => tag.Naam).ToList()
                });
            }

            ProjectFilterListViewModel vm = new ProjectFilterListViewModel
            {
                Projecten = projecten
            };
            return View(vm);
        }

        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(ProjectCreateViewModel model)
        {
            if (!TryValidateModel(model))
            {
                return View();
            }

            Project project = new Project()
            {
                Titel = model.Titel,
                Beschrijving = model.Beschrijving,
                Status = _context.Status.SingleOrDefault(s => s.Id == model.Status)
            };

            using (var memoryStream = new MemoryStream())
            {
                await model.Image.CopyToAsync(memoryStream);
                project.Image = memoryStream.ToArray();
            }

            List<string> newTags = new List<string>();
            foreach (var tag in model.Tags.Split(' '))
            {
                newTags.Add(tag.Trim());
            }

            _context.Insert(project);
            _context.AssignTags(newTags, project.Id);

            return RedirectToAction("Project", new { id = project.Id });
        }

        public IActionResult Project(int id)
        {
            Project projectFromDb = _context.GetProject(id);
            ProjectDetailsViewModel model = new ProjectDetailsViewModel()
            {
                Titel = projectFromDb.Titel,
                Beschrijving = projectFromDb.Beschrijving,
                Status = projectFromDb.Status.Naam,
                ImageFile = projectFromDb.Image,
                Tags = projectFromDb.TagProjects.Select(tagprojects => tagprojects.Tag).Select(tag => tag.Naam).ToList()
            };
            return View(model);
        }

        [Authorize]
        public IActionResult Update(int id)
        {
            Project projectFromDb = _context.GetProject(id);
            ProjectUpdateViewModel vm = new ProjectUpdateViewModel
            {
                Titel = projectFromDb.Titel,
                Beschrijving = projectFromDb.Beschrijving,
                Status = projectFromDb.Status.Id,
                Image = projectFromDb.Image,
                Tags = string.Join(" ", projectFromDb.TagProjects.Select(t => t.Tag).Select(t => t.Naam).ToList())
            };
            return View(vm);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Update(int id, ProjectUpdateViewModel model)
        {
            if (!TryValidateModel(model))
            {
                return View(model);
            }

            Project projectToUpdate = _context.Projecten.SingleOrDefault(p => p.Id == id);

            projectToUpdate.Titel = model.Titel;
            projectToUpdate.Beschrijving = model.Beschrijving;
            projectToUpdate.Status = _context.Status.SingleOrDefault(s => s.Id == model.Status);

            using (var memoryStream = new MemoryStream())
            {
                await model.newImage.CopyToAsync(memoryStream);
                projectToUpdate.Image = memoryStream.ToArray();
            }

            List<string> newTags = new List<string>();
            foreach (var tag in model.Tags.Split(' '))
            {
                newTags.Add(tag.Trim());
            }

            _context.AssignTags(newTags, projectToUpdate.Id);
            _context.Update(id, projectToUpdate);

            return RedirectToAction("Project", new { id = projectToUpdate.Id });
        }

        [Authorize]
        public IActionResult Delete(int id)
        {
            Project projectFromDb = _context.GetProject(id);
            ProjectDeleteViewModel model = new ProjectDeleteViewModel()
            {
                Titel = projectFromDb.Titel,
                Id = projectFromDb.Id
            };
            return View(model);
        }

        [Authorize]
        [HttpPost]
        public IActionResult ConfirmDelete(int id)
        {
            _context.Delete(id);
            return RedirectToAction("Beheer");
        }

        [Authorize]
        public IActionResult Beheer()
        {
            List<ProjectListViewModel> projecten = new List<ProjectListViewModel>();
            IEnumerable<Project> projectenFromDatabase = _context.GetProjecten();

            foreach (var Project in projectenFromDatabase)
            {
                projecten.Add(new ProjectListViewModel()
                {
                    Id = Project.Id,
                    Titel = Project.Titel,
                    Status = Project.Status.Naam
                });
            }

            return View(projecten);
        }


        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
