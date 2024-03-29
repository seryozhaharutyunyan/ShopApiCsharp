﻿using Models;

namespace Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> RetrieveAllAsync();
        Task<User?> RetrieveAsync(int id);
        User? Retrieve(string email);
        Task<User?> CreateAsync(User data);
        Task<User?> UpdateAsync(int id, User data);
        Task<bool> DeleteAsync(int id);
    }
}
