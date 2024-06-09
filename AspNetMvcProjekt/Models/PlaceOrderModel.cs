using System.ComponentModel.DataAnnotations;

namespace AspNetMvcProjekt.Web.Models;

public record PlaceOrderModel(
	string City,
	string Address,
	[RegularExpression(@"\d{5}", ErrorMessage = "The zip code must consist of 5 digits")]
	string ZipCode
);
