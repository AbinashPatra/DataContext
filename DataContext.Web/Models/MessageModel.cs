using System;
using System.ComponentModel.DataAnnotations;

namespace DataContext.Web.Models
{
	public class MessageModel
	{
		[Key]
		public int Id { get; set; }
		public string Title { get; set; }
		public string Body { get; set; }
		public DateTime EndDate { get; set; }

	}

}
