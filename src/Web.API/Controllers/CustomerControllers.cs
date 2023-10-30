using Application.Customers.Create;
using Application.Customers.Delete;
using Application.Customers.GetAll;
using Application.Customers.GetById;
using Application.Customers.Update;
using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;

namespace Web.API.Controllers;

[Route("customers")]
public class CustomerControllers : ApiController
{
    private readonly ISender _mediatr;

    public CustomerControllers(ISender mediatr)
    {
        _mediatr = mediatr ?? throw new ArgumentNullException(nameof(mediatr));
    }

    [HttpGet]
    public async ValueTask<IActionResult> GetAll()
    {
        var customerResult = await _mediatr.Send(new GetAllCustomersQuery());

        return customerResult.Match(
            Ok,
            Problem
        );
    }

    [HttpGet("{id}")]
    public async ValueTask<IActionResult> GetById(Guid id)
    {
        var customerResult = await _mediatr.Send(new GetCustomerByIdQuery(id));

        return customerResult.Match(
            Ok,
            Problem
        );
    }

    [HttpPost]
    public async ValueTask<IActionResult> Create([FromBody] CreateCustomerCommand command)
    {
        var customerResult = await _mediatr.Send(command);

        return customerResult.Match(
            customer => Ok(),
            Problem
        );
    }

    [HttpPut("{id}")]
    public async ValueTask<IActionResult> Update(Guid id, [FromBody] UpdateCustomerCommand command)
    {
        if (command.Id != id)
        {
            List<Error> errors = new()
            {
                Error.Validation("Customer.UpdateInvalid", "The request Id does not match with the url Id.")
            };

            return Problem(errors);
        }

        var updateResult = await _mediatr.Send(command);

        return updateResult.Match(
            customerId => NoContent(),
            Problem
        );
    }

    [HttpDelete("{id}")]
    public async ValueTask<IActionResult> Delete(Guid id)
    {
        var deleteResult = await _mediatr.Send(new DeleteCustomerCommand(id));

        return deleteResult.Match(
            customerId => NoContent(),
            Problem
        );
    }
}