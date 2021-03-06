﻿using Web.Interfaces;
using System.Collections.Generic;

namespace Web.Models
{
    public class Role : IModel
    {
        #region Properties

        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        #endregion

        #region Navigation

        public virtual ICollection<User> Users { get; set; }

        #endregion

        /// <summary>
        /// Returns this models Resource URI name
        /// </summary>
        /// <returns></returns>
        public string Resource()
        {
            return "roles";
        }
    }
}