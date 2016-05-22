using System.ComponentModel.DataAnnotations;
using EStore.BL.Models._Common;

namespace EStore.BL.Models
{
    public class CheckOutModel : ViewModelBase
    {
        public long? Id { get; set; }

        /// <summary>
        /// Доставка или самовывоз
        /// </summary>
        public bool? IsDelivery { get; set; }

        [Required(ErrorMessage = "Необходимо заполнить город")]
        public string City { get; set; }

        [Required(ErrorMessage = "Необходимо заполнить адрес доставки")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Необходимо заполнить имя")]
        public string Name { get; set; }

        public string Phone { get; set; }

        [EmailAddress(ErrorMessage = "Формат электронной почты неверный")]
        public string Email { get; set; }
    }
}