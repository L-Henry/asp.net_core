using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Portfolio.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Portfolio.Data
{
    public class ProjectContext : IdentityDbContext
    {
        public ProjectContext(DbContextOptions<ProjectContext> options) : base(options)
        {

        }


        public DbSet<Project> Projecten { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<TagProject> TagProject { get; set; }
        public DbSet<Status> Status { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TagProject>().HasKey(tagProject => new { tagProject.TagId, tagProject.ProjectId });
            base.OnModelCreating(modelBuilder);
        }

        public IEnumerable<Project> GetProjecten()
        {
            return Projecten
                .Include(x => x.Status)
                .Include(x => x.TagProjects).ThenInclude(x => x.Tag);
        }

        public Project GetProject(int id)
        {
            return Projecten
                .Include(x => x.Status)
                .Include(p=>p.TagProjects).ThenInclude(p=>p.Tag)
                .SingleOrDefault(p => p.Id == id);
        }

        public Project Insert(Project project)
        {
            Projecten.Add(project);
            this.SaveChanges();
            return project;
        }

        public void AssignTags(List<string> tags, int id)
        {
            foreach (var tag in tags)
            {
                if (Tags.Any(t => t.Naam.ToLower() == tag.ToLower().Trim()))
                {
                    //TagProject.Add(new TagProject { Tag = Tags.SingleOrDefault(t => t.Naam.ToLower() == tag.ToLower().Trim()), Project = Projecten.SingleOrDefault(p => p.Id == id) });
                    Projecten
                        .Include(x => x.TagProjects).ThenInclude(x => x.Tag)
                        .SingleOrDefault(p => p.Id == id)
                        .TagProjects.Add(new TagProject { TagId = Tags.SingleOrDefault(t => t.Naam.ToLower() == tag.ToLower().Trim()).Id, ProjectId = Projecten.SingleOrDefault(p => p.Id == id).Id });
                }
                else
                {
                    Tag newTag = new Tag { Naam = tag.Trim().ToLower() };
                    Tags.Add(newTag);
                    Projecten
                        .Include(x=>x.TagProjects).ThenInclude(x=>x.Tag)
                        .SingleOrDefault(p => p.Id == id)
                        .TagProjects.Add(new TagProject { Tag = new Tag { Naam = tag.ToLower().Trim() }, ProjectId = Projecten.SingleOrDefault(p => p.Id == id).Id });
                    //TagProject.Add(new TagProject { Tag = Tags.SingleOrDefault(t => t.Naam.ToLower() == tag.ToLower().Trim()), Project = Projecten.SingleOrDefault(p => p.Id == id) });
                }
            }
            this.SaveChanges();
        }

        public void Update(int id, Project updatedProject) {
            var project = Projecten.SingleOrDefault(p => p.Id == id);
            if (project != null)
            {
                project.Titel = updatedProject.Titel;
                project.Beschrijving = updatedProject.Beschrijving;
                project.Status = updatedProject.Status;
            }
            this.SaveChanges();
        }

        public void Delete(int id) {
            var project = Projecten.SingleOrDefault(x => x.Id == id);
            if (project != null)
            {
                Projecten.Remove(project);
                this.SaveChanges();
            }
        }

    }
}
