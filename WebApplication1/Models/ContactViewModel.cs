using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace WebApplication1.Models
{
	public class ContactViewModel
	{
		[Required(ErrorMessage = "Hãy nhập tên của bạn.")]
		[Display(Name = "Tên của bạn")]
		public string Name { get; set; }

		[Required(ErrorMessage = "Hãy nhập địa chỉ email của bạn.")]
		[EmailAddress(ErrorMessage = "Địa chỉ email không hợp lệ.")]
		[Display(Name = "Địa chỉ email của bạn")]
		public string Email { get; set; }

		[Required(ErrorMessage = "Hãy nhập nội dung tin nhắn.")]
		[Display(Name = "Nội dung tin nhắn")]
		public string Message { get; set; }

		[Display(Name = "Số điện thoại")]
		public string Phone { get; set; }

		public MailSettings ToMailSettings()
		{
			return new MailSettings
			{
				Host = "smtp.gmail.com",
				Port = 587,
				UserName = "daoanhquan2k3@gmail.com",
				Password = "kimquan1@",
				FromName = "Quan",
				FromEmail = "daoanhquan2k3@gmail.com"
			};
		}
	}
}
