using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace LHFS.Models
{ 
    public class GalleryImageRepository : IGalleryImageRepository
    {
        LHFSContext context = new LHFSContext();

        public IQueryable<GalleryImage> All
        {
			get { return context.GalleryImage; }
        }

        public IQueryable<GalleryImage> AllIncluding(params Expression<Func<GalleryImage, object>>[] includeProperties)
        {
            IQueryable<GalleryImage> query = context.GalleryImage;
            foreach (var includeProperty in includeProperties) {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public GalleryImage Find(int id)
        {
            return context.GalleryImage.Find(id);
        }

        public void InsertOrUpdate(GalleryImage galleryimage)
        {
            if (galleryimage.ID == default(int)) {
                // New entity
                context.GalleryImage.Add(galleryimage);
            } else {
                // Existing entity
                context.GalleryImage.Attach(galleryimage);
                context.Entry(galleryimage).State = EntityState.Modified;
            }
        }

        public void Delete(int id)
        {
            var galleryimage = context.GalleryImage.Find(id);
            context.GalleryImage.Remove(galleryimage);
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }

	public interface IGalleryImageRepository
    {
		IQueryable<GalleryImage> All { get; }
		IQueryable<GalleryImage> AllIncluding(params Expression<Func<GalleryImage, object>>[] includeProperties);
		GalleryImage Find(int id);
		void InsertOrUpdate(GalleryImage galleryimage);
        void Delete(int id);
        void Save();
    }
}