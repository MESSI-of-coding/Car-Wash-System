using Microsoft.AspNetCore.Mvc;
using CarWash.BL.Services;
using CarWash.Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ClosedXML.Excel;
using System.IO;

namespace CarWash.API.Controllers
{
    [ApiController]
    [Route("api/reports")]
    public class ReportsController : ControllerBase
    {
        private readonly IWashRequestService _washRequestService;

        public ReportsController(IWashRequestService washRequestService)
        {
            _washRequestService = washRequestService;
        }

        /// <summary>
        /// Retrieves wash requests filtered by date range and service type.
        /// </summary>
        /// <param name="startDate">The start date of the filter range.</param>
        /// <param name="endDate">The end date of the filter range.</param>
        /// <param name="serviceType">The service type ID to filter by.</param>
        /// <returns>A list of filtered wash requests.</returns>
        [HttpGet("requests")]
        public async Task<ActionResult<List<WashRequest>>> GetFilteredWashRequests(
            [FromQuery] DateTime startDate,
            [FromQuery] DateTime endDate,
            [FromQuery] int serviceType)
        {
            var filteredRequests = await _washRequestService.GetFilteredWashRequestsAsync(startDate, endDate, serviceType);
            return Ok(filteredRequests);
        }

        /// <summary>
        /// Exports wash requests filtered by date range and service type to an Excel file.
        /// </summary>
        /// <param name="startDate">The start date of the filter range.</param>
        /// <param name="endDate">The end date of the filter range.</param>
        /// <param name="serviceType">The service type ID to filter by.</param>
        /// <returns>An Excel file containing the filtered wash requests.</returns>
        [HttpGet("export")]
        public async Task<IActionResult> ExportFilteredWashRequests(
            [FromQuery] DateTime startDate,
            [FromQuery] DateTime endDate,
            [FromQuery] int serviceType)
        {
            var filteredRequests = await _washRequestService.GetFilteredWashRequestsAsync(startDate, endDate, serviceType);

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Filtered Wash Requests");

                // Add headers
                worksheet.Cell(1, 1).Value = "Request ID";
                worksheet.Cell(1, 2).Value = "User Name";
                worksheet.Cell(1, 3).Value = "Service Type";
                worksheet.Cell(1, 4).Value = "Scheduled Date";

                // Add data
                for (int i = 0; i < filteredRequests.Count; i++)
                {
                    var request = filteredRequests[i];
                    worksheet.Cell(i + 2, 1).Value = request.RequestId;
                    worksheet.Cell(i + 2, 2).Value = request.Customer?.FullName;
                    worksheet.Cell(i + 2, 3).Value = request.Package?.PackageName;
                    worksheet.Cell(i + 2, 4).Value = request.ScheduledDateTime;
                }

                // Save to memory stream
                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    stream.Seek(0, SeekOrigin.Begin);

                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "FilteredWashRequests.xlsx");
                }
            }
        }
    }
}