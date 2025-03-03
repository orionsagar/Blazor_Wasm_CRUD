﻿using BlazorCrud.Server.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
namespace BlazorCrud.Client.Pages
{
    public class EmployeeDataBase : ComponentBase
    {
        [Inject]
        HttpClient Http { get; set; }

        protected List<Employee> empList = new();
        protected List<Employee> searchEmpData = new();
        protected Employee emp = new();
        protected string SearchString { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await GetEmployee();
        }

        protected async Task GetEmployee()
        {
            empList = await Http.GetFromJsonAsync<List<Employee>>("api/Employee");
            searchEmpData = empList;
        }

        protected void FilterEmp()
        {
            if (!string.IsNullOrEmpty(SearchString))
            {
                empList = searchEmpData
                    .Where(x => x.Name.IndexOf(SearchString, StringComparison.OrdinalIgnoreCase) != -1)
                    .ToList();
            }
            else
            {
                empList = searchEmpData;
            }
        }

        protected void DeleteConfirm(int empID)
        {
            emp = empList.FirstOrDefault(x => x.EmployeeId == empID);
        }

        protected async Task DeleteEmployee(int empID)
        {
            await Http.DeleteAsync("api/Employee/" + empID);
            await GetEmployee();
        }
    }
}
