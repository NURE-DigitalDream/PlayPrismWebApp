﻿using PlayPrism.Core.Models;

namespace PlayPrism.Contracts.V1.Requests.Products;

public class GetProductsRequest
{
    public PageInfo PageInfo { get; set; }
    public Filter[] Filters { get; set; }

}