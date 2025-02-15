﻿using Domain.Models;
using MediatR;

namespace Application.Queries.Authors.GetAll
{
    public class GetAllAuthorsQuery : IRequest<OperationResult<List<Author>>>
    {
    }
}
