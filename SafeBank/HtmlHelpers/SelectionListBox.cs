﻿using System.Collections.Generic;
using System.Web.Mvc;
using System.Linq.Expressions;
using System;
using System.Linq;
using System.Text;

namespace SafeBank.HtmlHelpers
{
    public static class SelectionListBox
    {

        public static MvcHtmlString SelectionListBoxFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, IEnumerable<object> listItems)
        {
            var metadata = ModelMetadata.FromLambdaExpression(expression, html.ViewData);
            var htmlFieldName = ExpressionHelper.GetExpressionText(expression);
            var modelPropertyName = htmlFieldName.Replace(".","_");
            if (string.IsNullOrEmpty(modelPropertyName))
            {
                return MvcHtmlString.Empty;
            }
            var selectList = new TagBuilder("select");
            selectList.Attributes.Add("name", htmlFieldName);
            selectList.Attributes.Add("id", modelPropertyName);
            if (listItems == null) return MvcHtmlString.Create(selectList.ToString());
            var options = new StringBuilder();
            foreach (var listItem in listItems)
            {
                var option = new TagBuilder("option") { InnerHtml = listItem.ToString() };
                option.Attributes.Add("value", listItem.ToString());
                if (metadata.Model != null && metadata.Model.ToString() == listItem.ToString())
                {
                    option.Attributes.Add("selected","");
                }
                options.AppendLine(option.ToString());
            }
            selectList.InnerHtml = options.ToString();
            return MvcHtmlString.Create(selectList.ToString());
        }
        
    }
}