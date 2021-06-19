﻿using System;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Core.Arango.Serialization.Newtonsoft
{
    /// <summary>
    ///     Arango Json Serializer with Newtonsoft
    /// </summary>
    public class ArangoNewtonsoftSerializer : IArangoSerializer
    {
        private readonly JsonSerializerSettings _settings;

        /// <summary>
        ///     Arango Json Serializer with Newtonsoft
        /// </summary>
        /// <param name="resolver">PascalCase or camelCaseResolver</param>
        public ArangoNewtonsoftSerializer(IContractResolver resolver)
        {
            _settings = new JsonSerializerSettings
            {
                ContractResolver = resolver,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                Formatting = Formatting.None,
                DateFormatHandling = DateFormatHandling.IsoDateFormat,
                DateTimeZoneHandling = DateTimeZoneHandling.Utc
            };
        }

        /// <inheritdoc />
        public byte[] Serialize(object value)
        {
            return Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(value, _settings));
        }

        /// <inheritdoc />
        public T Deserialize<T>(byte[] value)
        {
            return JsonConvert.DeserializeObject<T>(Encoding.UTF8.GetString(value), _settings);
        }

        /// <inheritdoc />
        public object Deserialize(byte[] v, Type t)
        {
            return JsonConvert.DeserializeObject(Encoding.UTF8.GetString(v), t, _settings);
        }

        public string ContentType => "application/json";
    }
}