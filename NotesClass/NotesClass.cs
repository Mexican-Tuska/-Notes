using System;
using System.Collections.Generic;
using System.IO;

namespace NotesClass
{
    public class NotesClass
    {
       
        Dictionary<string, FileStream> notes;  
        NotesClass ()
        {
            
        }
        NotesClass (int a)
        {
            notes = new Dictionary<string, FileStream>(a);
        }
    }
}
