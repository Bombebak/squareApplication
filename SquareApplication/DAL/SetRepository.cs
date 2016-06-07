﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Scripting.Interpreter;
using SquareApplication.Models;

namespace SquareApplication.DAL
{
    public class SetRepository
    {
        private SquareDbEntities db = new SquareDbEntities();

        public IEnumerable<UserActivityViewModel> GetSetsFromUser(int userId)
        {
            return (from st in db.Set_Tag
                join s in db.Sets on st.settag_id equals s.set_id
                where s.user_id == userId
                select new UserActivityViewModel()
                {
                    SetTitle = s.name,
                    UploadDate = (DateTime) s.upload_date,
                    SetId = s.set_id
                });
        }

        public IEnumerable<Set> GetAllSets()
        {
            return null;
        } 
    }
}