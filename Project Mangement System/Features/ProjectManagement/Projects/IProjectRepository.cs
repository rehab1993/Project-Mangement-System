using Project_Mangement_System.Models;
using System.Linq.Expressions;

namespace Project_Mangement_System.Features.ProjectManagement.Projects
{
    public interface IProjectRepository<T> where T : BaseModel
    {

       
            //Add
            Task<T> AddAsync(T model);

            //Get All
            IQueryable<T> GetAll();
            IQueryable<T> Get(Expression<Func<T, bool>> predicate);

            //Get By Id 
            Task<T> GetByIdAsync(int id);

            Task<bool> GetAsyncAny(Expression<Func<T, bool>> predicate);

            //Update  
            bool Update(int id, T model);
            //Update Include
            void UpdateInclude(T entity, params string[] modifiedProperties);


            //Delete
            Task<bool> Delete(int id);



            Task<int> SaveChangesAsync();
        
    }
}
