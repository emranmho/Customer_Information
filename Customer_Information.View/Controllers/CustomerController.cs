using Customer_Information.View.Models;
using Customer_Information.View.Models.Dto;
using Customer_Information.View.Models.ViewModel;
using Customer_Information.View.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

namespace Customer_Information.View.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ICustomerService _customerService;
        private readonly ICountryService _countryService;
        private readonly IAddressService _addressService;
        public CustomerController(ICustomerService customerService,ICountryService countryService, IAddressService addressService)
        {
            _customerService = customerService;
            _countryService = countryService;
            _addressService = addressService;
        }

        public async Task<IActionResult> CustomerInfo(int? customerId)
        {
            CustomerDtoVM allList;
            if (customerId == null)
            {
                allList = new();
                
                var responseCustomer = await _customerService.GetAll<ApiResponse>();
                if (responseCustomer != null && responseCustomer.IsSussess)
                {
                    allList.Customerlist = JsonConvert.DeserializeObject<List<CustomerDto>>(Convert.ToString(responseCustomer.Result));
                }

                var responseCountry = await _countryService.GetAll<ApiResponse>();
                if (responseCountry != null && responseCountry.IsSussess)
                {
                    allList.CountryList = JsonConvert.DeserializeObject<List<CountryDto>>(Convert.ToString(responseCountry.Result)).Select(i => new SelectListItem
                    {
                        Text = i.CountryName,
                        Value = i.Id.ToString()
                    });
                }
                return View(allList);
            }
            else
            {
                allList = new CustomerDtoVM();

                var responseCustomerAll = await _customerService.GetAll<ApiResponse>();
                if (responseCustomerAll != null && responseCustomerAll.IsSussess)
                {
                    allList.Customerlist = JsonConvert.DeserializeObject<List<CustomerDto>>(Convert.ToString(responseCustomerAll.Result));
                }
                
                var responseCountryAll = await _countryService.GetAll<ApiResponse>();
                if (responseCountryAll != null && responseCountryAll.IsSussess)
                {
                    allList.CountryList = JsonConvert.DeserializeObject<List<CountryDto>>(Convert.ToString(responseCountryAll.Result)).Select(i => new SelectListItem
                    {
                        Text = i.CountryName,
                        Value = i.Id.ToString()
                    });
                }

                var responseCustomer = await _customerService.Get<ApiResponse>(customerId.Value);
                if (responseCustomer != null && responseCustomer != null)
                {  
                    allList.Customer = JsonConvert.DeserializeObject<CustomerDto>(Convert.ToString(responseCustomer.Result));
                }

                var responseAddress = await _addressService.GetCustomerAddress<ApiResponse>(customerId.Value);
                if (responseAddress != null && responseAddress != null)
                {
                    allList.Address = JsonConvert.DeserializeObject<CustomerAddressDto>(Convert.ToString(responseAddress.Result));
                }

                if (allList!=null)
                {
                    return View(allList);
                }
                return NotFound();
            }
        }


       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CustomerInfoUpsert(CustomerDtoVM obj, string? action)
        {
            if(action != null)
            {
                if (action == "edit")
                {
                    if (obj.Customer.Id != 0)
                    {
                        if (ModelState.IsValid)
                        {
                            if (Request.Form.Keys.Contains("radio1"))
                            {
                                obj.Customer.MeritialStatus = 0;
                            }
                            else if (Request.Form.Keys.Contains("radio2"))
                            {
                                obj.Customer.MeritialStatus = 1;
                            }
                            else
                            {
                                obj.Customer.MeritialStatus = 2;
                            }
                            var response = await _customerService.Update<ApiResponse>(obj.Customer);
                            if (response != null && response.IsSussess)
                            {
                                return RedirectToAction(nameof(CustomerInfo), obj.Customer.Id);
                            }
                            else
                            {
                                if (response.ErrorMessage.Count > 0)
                                {
                                    ModelState.AddModelError("Error", response.ErrorMessage.FirstOrDefault());
                                }
                            }
                        }
                        return RedirectToAction(nameof(CustomerInfo));
                    }
                }
                else if (action == "add")
                {
                    if (ModelState.IsValid)
                    {
                        if (Request.Form.Keys.Contains("radio1"))
                        {
                            obj.Customer.MeritialStatus = 0;
                        }
                        else if (Request.Form.Keys.Contains("radio2"))
                        {
                            obj.Customer.MeritialStatus = 1;
                        }
                        else
                        {
                            obj.Customer.MeritialStatus = 2;
                        }
                        var response = await _customerService.Create<ApiResponse>(obj.Customer);
                        if (response != null && response.IsSussess)
                        {
                            return RedirectToAction(nameof(CustomerInfo), obj.Customer.Id);
                        }
                        else
                        {
                            if (response.ErrorMessage.Count > 0)
                            {
                                ModelState.AddModelError("ErrorMessages", response.ErrorMessage.FirstOrDefault());
                            }
                        }
                    }

                    return RedirectToAction(nameof(CustomerInfo));
                }
                else if (action == "delete")
                {
                    if (obj.Customer.Id != 0)
                    {
                        var response = await _customerService.Remove<ApiResponse>(obj.Customer.Id);
                        if (response != null && response.IsSussess)
                        {
                            return RedirectToAction(nameof(CustomerInfo));
                        }
                    }
                }
                else if (action == "editAddress")
                {
                    if (obj.Customer.Id != 0)
                    {
                        var response = await _addressService.Update<ApiResponse>(obj.Address);
                        if (response != null && response.IsSussess)
                        {
                            return RedirectToAction(nameof(CustomerInfo), obj.Address.CustomerId);
                        }
                        else
                        {
                            if (response.ErrorMessage.Count > 0)
                            {
                                ModelState.AddModelError("Error", response.ErrorMessage.FirstOrDefault());
                            }
                        }
                    }
                }
                else if (action == "deleteAddress")
                {
                    if (obj.Customer.Id != 0 && obj.Address.Id !=0)
                    {
                        var response = await _addressService.Remove<ApiResponse>(obj.Address.Id);
                        if (response != null && response.IsSussess)
                        {
                            return RedirectToAction(nameof(CustomerInfo));
                        }
                    }
                }
                else if (action == "addAddress")
                {
                    if (ModelState.IsValid)
                    {
                        var add = obj.Address;
                        add.CustomerId = obj.Customer.Id;
                        var response = await _addressService.Create<ApiResponse>(add);
                        if (response != null && response.IsSussess)
                        {
                            return RedirectToAction(nameof(CustomerInfo), obj.Customer.Id);
                        }
                        else
                        {
                            if (response.ErrorMessage.Count > 0)
                            {
                                ModelState.AddModelError("ErrorMessages", response.ErrorMessage.FirstOrDefault());
                            }
                        }
                    }

                    return RedirectToAction(nameof(CustomerInfo));
                }
            }
            return RedirectToAction(nameof(CustomerInfo));
        }
    }
}
