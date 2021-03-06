﻿using System.Net.Http;
using System.Threading.Tasks;
using EnsureThat;
using MyCouch.Extensions;
using MyCouch.HttpRequestFactories;
using MyCouch.Net;
using MyCouch.Requests;
using MyCouch.Responses;
using MyCouch.Responses.Factories;
using MyCouch.Serialization;

namespace MyCouch.Contexts
{
    public class Views : ApiContextBase<IDbClientConnection>, IViews
    {
        protected QueryViewHttpRequestFactory QueryViewHttpRequestFactory { get; set; }
        protected ViewQueryResponseFactory ViewQueryResponseFactory { get; set; }

        public Views(IDbClientConnection connection, ISerializer serializer)
            : base(connection)
        {
            Ensure.That(serializer, "serializer").IsNotNull();

            QueryViewHttpRequestFactory = new QueryViewHttpRequestFactory(serializer);
            ViewQueryResponseFactory = new ViewQueryResponseFactory(serializer);
        }

        public virtual async Task<ViewQueryResponse> QueryAsync(QueryViewRequest request)
        {
            var httpRequest = CreateHttpRequest(request);

            using (var res = await SendAsync(httpRequest).ForAwait())
            {
                return ProcessHttpResponse(res);
            }
        }

        public virtual async Task<ViewQueryResponse<TValue>> QueryAsync<TValue>(QueryViewRequest request)
        {
            var httpRequest = CreateHttpRequest(request);

            using (var res = await SendAsync(httpRequest).ForAwait())
            {
                return ProcessHttpResponse<TValue>(res);
            }
        }

        public virtual async Task<ViewQueryResponse<TValue, TIncludedDoc>> QueryAsync<TValue, TIncludedDoc>(QueryViewRequest request)
        {
            var httpRequest = CreateHttpRequest(request);

            using (var res = await SendAsync(httpRequest).ForAwait())
            {
                return ProcessHttpResponse<TValue, TIncludedDoc>(res);
            }
        }

        protected virtual QueryViewRequest CreateQueryViewRequest(string designDocument, string viewname)
        {
            return new QueryViewRequest(designDocument, viewname);
        }
        
        protected virtual HttpRequest CreateHttpRequest(QueryViewRequest request)
        {
            return QueryViewHttpRequestFactory.Create(request);
        }

        protected virtual ViewQueryResponse ProcessHttpResponse(HttpResponseMessage response)
        {
            return ViewQueryResponseFactory.Create(response);
        }

        protected virtual ViewQueryResponse<T> ProcessHttpResponse<T>(HttpResponseMessage response)
        {
            return ViewQueryResponseFactory.Create<T>(response);
        }

        protected virtual ViewQueryResponse<TValue, TIncludedDoc> ProcessHttpResponse<TValue, TIncludedDoc>(HttpResponseMessage response)
        {
            return ViewQueryResponseFactory.Create<TValue, TIncludedDoc>(response);
        }
    }
}