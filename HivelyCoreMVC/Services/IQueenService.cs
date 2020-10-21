using HivelyCoreMVC.Models.QueenModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HivelyCoreMVC.Services
{
    public interface IQueenService
    {
        Task<bool> CreateQueen(QueenCreate model);
        Task<bool> DeleteQueen(int id);
        Task<QueenDetails> GetQueenById(int id);
        Task<IEnumerable<QueenListItem>> GetQueens();
        void SetUserId(Guid userId);
        Task<bool> UpdateQueen(QueenEdit model);
    }
}