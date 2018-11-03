using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace OpenTracing.Contrib.SemanticConventions
{
    /// <summary>
    /// Extensions for <see cref="ISpan.SetTag(string,string)"/> based on <see cref="KnownTagNames"/>
    /// </summary>
    [PublicAPI]
    public static class SetTagBasedExtensions
    {
        /// <seealso cref="KnownTagNames.Peer"/>
        public static ISpan SetPeerTags(this ISpan span, Peer peer)
        {
            return span
                .SetTag(KnownTagNames.Peer.Port, peer.Port)
                .SetTag(KnownTagNames.Peer.Address, peer.Address)
                .SetTag(KnownTagNames.Peer.Hostname, peer.Hostname)
                .SetTag(KnownTagNames.Peer.Ipv4, peer.Ipv4)
                .SetTag(KnownTagNames.Peer.Ipv6, peer.Ipv6)
                .SetTag(KnownTagNames.Peer.Service, peer.Service);
        }

        /// <seealso cref="KnownTagNames.Db"/>
        public static ISpan SetDbTags(this ISpan span, Db db)
        {
            return span
                .SetTag(KnownTagNames.Db.Instance, db.Instance)
                .SetTag(KnownTagNames.Db.Statement, db.Statement)
                .SetTag(KnownTagNames.Db.Type, db.Type)
                .SetTag(KnownTagNames.Db.User, db.User);
        }

        /// <seealso cref="KnownTagNames.Http"/>
        public static ISpan SetHttpTags(this ISpan span, Http http)
        {
            return span
                .SetTag(KnownTagNames.Http.Method, http.Method)
                .SetTag(KnownTagNames.Http.Url, http.Url)
                .SetTag(KnownTagNames.Http.StatusCode, http.StatusCode);
        }

        /// <seealso cref="KnownTagNames.Component"/>
        public static ISpan SetComponentTag(this ISpan span, string component)
        {
            return span
                .SetTag(KnownTagNames.Component, component);
        }

        /// <seealso cref="KnownTagNames.Error"/>
        public static ISpan SetErrorTag(this ISpan span, string error)
        {
            return span
                .SetTag(KnownTagNames.Error, error);
        }

        /// <seealso cref="KnownTagNames.Service"/>
        public static ISpan SetServiceTag(this ISpan span, string service)
        {
            return span
                .SetTag(KnownTagNames.Service, service);
        }

        /// <seealso cref="KnownTagNames.MessageBus.Destination"/>
        public static ISpan SetMessageBusDestinationTags(this ISpan span, string destination)
        {
            return span
                .SetTag(KnownTagNames.MessageBus.Destination, destination);
        }

        /// <seealso cref="KnownTagNames.Span.Kind"/>
        public static ISpan SetSpanKindTag(this ISpan span, string kind)
        {
            return span
                .SetTag(KnownTagNames.Span.Kind, kind);
        }

        /// <seealso cref="KnownTagNames.Sampling.Priority"/>
        public static ISpan SetSamplingPriorityTag(this ISpan span, string priority)
        {
            return span
                .SetTag(KnownTagNames.Sampling.Priority, priority);
        }

        public struct Http
        {
            public readonly string Method;
            public readonly string Url;
            public readonly string StatusCode; // TODO: Is this specified to be an int? Need to check data types for all of the fields in this file

            public Http(string method, string url, string statusCode)
            {
                Method = method;
                Url = url;
                StatusCode = statusCode;
            }
        }

        public struct Db
        {
            public readonly string Instance;
            public readonly string Statement;
            public readonly string Type;
            public readonly string User;

            public Db(string instance, string statement, string type, string user)
            {
                Instance = instance;
                Statement = statement;
                Type = type;
                User = user;
            }
        }

        public struct Peer
        {
            public readonly string Port;
            public readonly string Address;
            public readonly string Hostname;
            public readonly string Ipv4;
            public readonly string Ipv6;
            public readonly string Service;

            public Peer(string port, string address, string hostname, string ipv4, string ipv6, string service)
            {
                Port = port;
                Address = address;
                Hostname = hostname;
                Ipv4 = ipv4;
                Ipv6 = ipv6;
                Service = service;
            }
        }
    }
}
