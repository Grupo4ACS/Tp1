using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tp1.Models;

namespace Tp1.Data
{
    public class UnitOfWork 
    {
        private readonly Tp1Context _context;

        public UnitOfWork(Tp1Context context)
        {
            this._context = context;
        }

        private GenericRepository<User> userRepository;
        private GenericRepository<Post> postRepository;

        public GenericRepository<User> UserRepository
        {
            get
            {
                if (this.userRepository == null)
                {
                    this.userRepository = new GenericRepository<User>(_context);
                }
                return this.userRepository;
            }
        }

        public GenericRepository<Post> PostRepository
        {
            get
            {
                if (this.postRepository == null)
                {
                    this.postRepository = new GenericRepository<Post>(_context);
                }
                return this.postRepository;
            }
        }

        public void save()
        {
            _context.SaveChanges();
        }
    }
}
