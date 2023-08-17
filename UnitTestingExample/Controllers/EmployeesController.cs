using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using UnitTestingExample.Business;
using UnitTestingExample.Models;

namespace UnitTestingExample.Controllers
{
    public class EmployeesController : Controller
    {
        //private readonly DemoDb2Context _context;

        private readonly IEmployeesBusiness _business;

        public EmployeesController(IEmployeesBusiness business)
        {
            _business = business;
        }

        // GET: Employees
        public async Task<IActionResult> Index()
        {
            var employees = await this._business.GetEmployees();
            //return _context.Employees != null ? 
            //            View(await _context.Employees.ToListAsync()) :
            //            Problem("Entity set 'DemoDb2Context.Employees'  is null.");
            return employees != null ?
                          View(employees.ToList()) :
                          Problem("Entity set 'DemoDb2Context.Employees'  is null.");
        }

        // GET: Employees/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var employee = await _context.Employees
            //    .FirstOrDefaultAsync(m => m.Id == id);

            var employee = await this._business.GetEmployeeById(id);

            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // GET: Employees/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Employees/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FirstName,LastName")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                //_context.Add(employee);
                //await _context.SaveChangesAsync();
                await this._business.CreateEmployee(employee);
                return RedirectToAction(nameof(Index));
            }
            return View(employee);
        }

        // GET: Employees/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await this._business.GetEmployeeById(id);
            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,LastName")] Employee employee)
        {
            if (id != employee.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    //_context.Update(employee);
                    //await _context.SaveChangesAsync();
                    await this._business.EditEmployee(employee);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!(await EmployeeExists(employee.Id)))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(employee);
        }

        // GET: Employees/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await this._business.GetEmployeeById(id);

            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var employees = await this._business.GetEmployees();
            if (employees == null || employees.Count() == 0)
            {
                return Problem("Entity set 'DemoDb2Context.Employees'  is null.");
            }
            var employee = await this._business.GetEmployeeById(id);
            if (employee != null)
            {
                await this._business.DeleteEmployee(employee);
            }
            
            //await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> EmployeeExists(int id)
        {
          return ((await this._business.GetEmployees()).Any(e => e.Id == id));
        }
    }
}
