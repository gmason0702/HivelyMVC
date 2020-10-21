using HivelyCoreMVC.Models.NoteModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HivelyCoreMVC.Services
{
    public interface INoteService
    {
        Task<bool> CreateNote(NoteCreate model);
        Task<bool> DeleteNote(int id);
        Task<NoteDetails> GetNoteById(int id);
        Task<IEnumerable<NoteListItem>> GetNotes();
        Task<bool> UpdateNote(NoteEdit model);
        void SetUserId(Guid userId);
    }
}