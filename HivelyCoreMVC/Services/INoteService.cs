using HivelyCoreMVC.Models.NoteModels;
using System.Collections.Generic;

namespace HivelyCoreMVC.Services
{
    public interface INoteService
    {
        bool CreateNote(NoteCreate model);
        bool DeleteNote(int id);
        NoteDetails GetNoteById(int id);
        IEnumerable<NoteListItem> GetNotes();
        bool UpdateNote(NoteEdit model);
    }
}