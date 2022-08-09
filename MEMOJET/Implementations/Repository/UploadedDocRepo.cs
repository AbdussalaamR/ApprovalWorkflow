using System.Collections.Generic;
using System.Net.Mime;
using System.Threading.Tasks;
using MEMOJET.Context;
using MEMOJET.Entities;
using MEMOJET.Interfaces.Repository;
using Microsoft.EntityFrameworkCore;

namespace MEMOJET.Implementations.Repository
{
    public class UploadedDocRepo:IUploadedDocRepo
    {
        private readonly ApplicationContext _context;

        public UploadedDocRepo(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<UploadedDoc> CreateDoc(UploadedDoc doc)
        {
            await _context.UploadedDocs.AddAsync(doc);
            await _context.SaveChangesAsync();
            return doc;
        }

        public async Task<UploadedDoc> UpdateDoc(UploadedDoc doc)
        {
            _context.UploadedDocs.Update(doc);
            await _context.SaveChangesAsync();
            return doc;
        }

        public async Task<UploadedDoc> GetDocument(int id)
        {
            var doc = await _context.UploadedDocs.Include(x => x.UserForm).FirstOrDefaultAsync(x => x.Id == id);
            return doc;
        }

        public async  Task<IList<UploadedDoc>> GetDocs()
        {
            var docs = await _context.UploadedDocs.ToListAsync();
            return docs;
        }
    }
}