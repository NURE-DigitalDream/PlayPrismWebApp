﻿using System.Linq.Expressions;
using PlayPrism.Core.Domain;

namespace PlayPrism.BLL.Constants;

/// <summary>
/// Represents selectors for emtities
/// </summary>
public static class EntitiesSelectors
{
   /// <summary>
   /// Gets or sets product selector
   /// </summary>
   public static Expression<Func<Product, Product>> ProductSelector => q => new Product
    {
        Name = q.Name,
        VariationOptions = q.VariationOptions.Select(option => new VariationOption
        {
            Id = option.Id,
            ProductConfiguration = option.ProductConfiguration,
            Value = option.Value,
        }).ToList(),
        ShortDescription = q.ShortDescription,
        DetailedDescription = q.DetailedDescription,
        Id = q.Id,
        ProductCategory = q.ProductCategory,
        ProductCategoryId = q.ProductCategoryId,
        HeaderImage = q.HeaderImage,
        ReleaseDate = q.ReleaseDate,
        Price = q.Price,
    };

    /// <summary>
    /// Gets or sets giveaway selector
    /// </summary>
    public static Expression<Func<Giveaway, Giveaway>> GiveawaySelector => q => new Giveaway
    {
        Id = q.Id,
        ProductId = q.ProductId,
        Participants = q.Participants.Select(participant => new UserProfile
        {
            Id = participant.Id,
            Nickname = participant.Nickname
        }).ToList(),
        WinnerId = q.WinnerId,
        ExpirationDate = q.ExpirationDate,
    };
}