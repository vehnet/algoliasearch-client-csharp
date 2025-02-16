/*
* Copyright (c) 2018 Algolia
* http://www.algolia.com/
*
* Permission is hereby granted, free of charge, to any person obtaining a copy
* of this software and associated documentation files (the "Software"), to deal
* in the Software without restriction, including without limitation the rights
* to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
* copies of the Software, and to permit persons to whom the Software is
* furnished to do so, subject to the following conditions:
*
* The above copyright notice and this permission notice shall be included in
* all copies or substantial portions of the Software.
*
* THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
* IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
* FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
* AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
* LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
* OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
* THE SOFTWARE.
*/

using System;
using System.Diagnostics;
using Algolia.Search.Models.Search;
using Algolia.Search.Utils;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Algolia.Search.Serializer
{
    /// <summary>
    /// Custom serializer for the query object because it must match specific syntax
    /// For more informations regarding the syntax
    /// https://www.algolia.com/doc/rest-api/search/#search-endpoints
    /// https://www.newtonsoft.com/json/help/html/JsonConverterAttributeClass.htm
    /// </summary>
    internal class QueryConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            Query query = (Query)value;

            string queryString = QueryStringHelper.ToQueryString(query);

            writer.WriteValue(queryString);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue,
            JsonSerializer serializer)
        {
            try
            {
                JObject jObject = JObject.Load(reader);
                Query target = new Query();

                serializer.Populate(jObject.CreateReader(), target);

                return target;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return null;
            }
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(Query);
        }
    }
}
