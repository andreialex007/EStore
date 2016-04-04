using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using EStore.BL.Models.Product;

namespace EStore.BL.Services
{
    public class AdminContext
    {
        #region Static

        static AdminContext()
        {
            if (HttpContext.Current.Session["AdminContext"] == null)
            {
                HttpContext.Current.Session["AdminContext"] = new AdminContext();
            }
        }

        public static AdminContext Current
        {
            get { return HttpContext.Current.Session["AdminContext"] as AdminContext; }
            set { HttpContext.Current.Session["AdminContext"] = value; }
        }

        #endregion

        public List<ProductFeedbackItem> Feedbacks { get; set; } = new List<ProductFeedbackItem>();
    }
}
