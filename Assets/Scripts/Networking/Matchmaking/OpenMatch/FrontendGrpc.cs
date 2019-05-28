// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: frontend.proto
// </auto-generated>
// Original file comments:
// Copyright 2018 Google LLC
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
//     http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//
#pragma warning disable 0414, 1591
#region Designer generated code

using grpc = global::Grpc.Core;

namespace Api {
  /// <summary>
  /// Call to start matchmaking for a player
  /// </summary>
  public static partial class Frontend
  {
    static readonly string __ServiceName = "api.Frontend";

    static readonly grpc::Marshaller<global::Api.CreatePlayerRequest> __Marshaller_api_CreatePlayerRequest = grpc::Marshallers.Create((arg) => global::Google.Protobuf.MessageExtensions.ToByteArray(arg), global::Api.CreatePlayerRequest.Parser.ParseFrom);
    static readonly grpc::Marshaller<global::Api.CreatePlayerResponse> __Marshaller_api_CreatePlayerResponse = grpc::Marshallers.Create((arg) => global::Google.Protobuf.MessageExtensions.ToByteArray(arg), global::Api.CreatePlayerResponse.Parser.ParseFrom);
    static readonly grpc::Marshaller<global::Api.DeletePlayerRequest> __Marshaller_api_DeletePlayerRequest = grpc::Marshallers.Create((arg) => global::Google.Protobuf.MessageExtensions.ToByteArray(arg), global::Api.DeletePlayerRequest.Parser.ParseFrom);
    static readonly grpc::Marshaller<global::Api.DeletePlayerResponse> __Marshaller_api_DeletePlayerResponse = grpc::Marshallers.Create((arg) => global::Google.Protobuf.MessageExtensions.ToByteArray(arg), global::Api.DeletePlayerResponse.Parser.ParseFrom);
    static readonly grpc::Marshaller<global::Api.GetUpdatesRequest> __Marshaller_api_GetUpdatesRequest = grpc::Marshallers.Create((arg) => global::Google.Protobuf.MessageExtensions.ToByteArray(arg), global::Api.GetUpdatesRequest.Parser.ParseFrom);
    static readonly grpc::Marshaller<global::Api.GetUpdatesResponse> __Marshaller_api_GetUpdatesResponse = grpc::Marshallers.Create((arg) => global::Google.Protobuf.MessageExtensions.ToByteArray(arg), global::Api.GetUpdatesResponse.Parser.ParseFrom);

    static readonly grpc::Method<global::Api.CreatePlayerRequest, global::Api.CreatePlayerResponse> __Method_CreatePlayer = new grpc::Method<global::Api.CreatePlayerRequest, global::Api.CreatePlayerResponse>(
        grpc::MethodType.Unary,
        __ServiceName,
        "CreatePlayer",
        __Marshaller_api_CreatePlayerRequest,
        __Marshaller_api_CreatePlayerResponse);

    static readonly grpc::Method<global::Api.DeletePlayerRequest, global::Api.DeletePlayerResponse> __Method_DeletePlayer = new grpc::Method<global::Api.DeletePlayerRequest, global::Api.DeletePlayerResponse>(
        grpc::MethodType.Unary,
        __ServiceName,
        "DeletePlayer",
        __Marshaller_api_DeletePlayerRequest,
        __Marshaller_api_DeletePlayerResponse);

    static readonly grpc::Method<global::Api.GetUpdatesRequest, global::Api.GetUpdatesResponse> __Method_GetUpdates = new grpc::Method<global::Api.GetUpdatesRequest, global::Api.GetUpdatesResponse>(
        grpc::MethodType.ServerStreaming,
        __ServiceName,
        "GetUpdates",
        __Marshaller_api_GetUpdatesRequest,
        __Marshaller_api_GetUpdatesResponse);

    /// <summary>Service descriptor</summary>
    public static global::Google.Protobuf.Reflection.ServiceDescriptor Descriptor
    {
      get { return global::Api.FrontendReflection.Descriptor.Services[0]; }
    }

    /// <summary>Base class for server-side implementations of Frontend</summary>
    public abstract partial class FrontendBase
    {
      /// <summary>
      /// CreatePlayer will put the player  in state storage, and then look
      /// through the 'properties' field for the attributes you have defined as
      /// indices your matchmaker config.  If the attributes exist and are valid
      /// integers, they will be indexed.
      /// INPUT: Player message with these fields populated:
      ///  - id
      ///  - properties
      /// OUTPUT: Result message denoting success or failure (and an error if
      /// necessary)
      /// </summary>
      /// <param name="request">The request received from the client.</param>
      /// <param name="context">The context of the server-side call handler being invoked.</param>
      /// <returns>The response to send back to the client (wrapped by a task).</returns>
      public virtual global::System.Threading.Tasks.Task<global::Api.CreatePlayerResponse> CreatePlayer(global::Api.CreatePlayerRequest request, grpc::ServerCallContext context)
      {
        throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
      }

      /// <summary>
      /// DeletePlayer removes the player from state storage by doing the
      /// following:
      ///  1) Delete player from configured indices.  This effectively removes the
      ///     player from matchmaking when using recommended MMF patterns.
      ///     Everything after this is just cleanup to save stage storage space.
      ///  2) 'Lazily' delete the player's state storage record.  This is kicked
      ///     off in the background and may take some time to complete.
      ///  2) 'Lazily' delete the player's metadata indicies (like, the timestamp when 
      ///     they called CreatePlayer, and the last time the record was accessed).  This 
      ///     is also kicked off in the background and may take some time to complete.
      /// INPUT: Player message with the 'id' field populated.
      /// OUTPUT: Result message denoting success or failure (and an error if
      /// necessary)
      /// </summary>
      /// <param name="request">The request received from the client.</param>
      /// <param name="context">The context of the server-side call handler being invoked.</param>
      /// <returns>The response to send back to the client (wrapped by a task).</returns>
      public virtual global::System.Threading.Tasks.Task<global::Api.DeletePlayerResponse> DeletePlayer(global::Api.DeletePlayerRequest request, grpc::ServerCallContext context)
      {
        throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
      }

      /// <summary>
      /// GetUpdates streams matchmaking results from Open Match for the
      /// provided player ID.
      /// INPUT: Player message with the 'id' field populated.
      /// OUTPUT: a stream of player objects with one or more of the following
      /// fields populated, if an update to that field is seen in state storage:
      ///  - 'assignment': string that usually contains game server connection information.
      ///  - 'status': string to communicate current matchmaking status to the client.
      ///  - 'error': string to pass along error information to the client.
      ///
      /// During normal operation, the expectation is that the 'assignment' field
      /// will be updated by a Backend process calling the 'CreateAssignments' Backend API
      /// endpoint.  'Status' and 'Error' are free for developers to use as they see fit.  
      /// Even if you had multiple players enter a matchmaking request as a group, the
      /// Backend API 'CreateAssignments' call will write the results to state
      /// storage separately under each player's ID. OM expects you to make all game
      /// clients 'GetUpdates' with their own ID from the Frontend API to get
      /// their results.
      ///
      /// NOTE: This call generates a small amount of load on the Frontend API and state
      ///  storage while watching the player record for updates. You are expected
      ///  to close the stream from your client after receiving your matchmaking
      ///  results (or a reasonable timeout), or you will continue to
      ///  generate load on OM until you do!
      /// NOTE: Just bear in mind that every update will send egress traffic from
      ///  Open Match to game clients! Frugality is recommended.
      /// </summary>
      /// <param name="request">The request received from the client.</param>
      /// <param name="responseStream">Used for sending responses back to the client.</param>
      /// <param name="context">The context of the server-side call handler being invoked.</param>
      /// <returns>A task indicating completion of the handler.</returns>
      public virtual global::System.Threading.Tasks.Task GetUpdates(global::Api.GetUpdatesRequest request, grpc::IServerStreamWriter<global::Api.GetUpdatesResponse> responseStream, grpc::ServerCallContext context)
      {
        throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
      }

    }

    /// <summary>Client for Frontend</summary>
    public partial class FrontendClient : grpc::ClientBase<FrontendClient>
    {
      /// <summary>Creates a new client for Frontend</summary>
      /// <param name="channel">The channel to use to make remote calls.</param>
      public FrontendClient(grpc::Channel channel) : base(channel)
      {
      }
      /// <summary>Creates a new client for Frontend that uses a custom <c>CallInvoker</c>.</summary>
      /// <param name="callInvoker">The callInvoker to use to make remote calls.</param>
      public FrontendClient(grpc::CallInvoker callInvoker) : base(callInvoker)
      {
      }
      /// <summary>Protected parameterless constructor to allow creation of test doubles.</summary>
      protected FrontendClient() : base()
      {
      }
      /// <summary>Protected constructor to allow creation of configured clients.</summary>
      /// <param name="configuration">The client configuration.</param>
      protected FrontendClient(ClientBaseConfiguration configuration) : base(configuration)
      {
      }

      /// <summary>
      /// CreatePlayer will put the player  in state storage, and then look
      /// through the 'properties' field for the attributes you have defined as
      /// indices your matchmaker config.  If the attributes exist and are valid
      /// integers, they will be indexed.
      /// INPUT: Player message with these fields populated:
      ///  - id
      ///  - properties
      /// OUTPUT: Result message denoting success or failure (and an error if
      /// necessary)
      /// </summary>
      /// <param name="request">The request to send to the server.</param>
      /// <param name="headers">The initial metadata to send with the call. This parameter is optional.</param>
      /// <param name="deadline">An optional deadline for the call. The call will be cancelled if deadline is hit.</param>
      /// <param name="cancellationToken">An optional token for canceling the call.</param>
      /// <returns>The response received from the server.</returns>
      public virtual global::Api.CreatePlayerResponse CreatePlayer(global::Api.CreatePlayerRequest request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return CreatePlayer(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      /// <summary>
      /// CreatePlayer will put the player  in state storage, and then look
      /// through the 'properties' field for the attributes you have defined as
      /// indices your matchmaker config.  If the attributes exist and are valid
      /// integers, they will be indexed.
      /// INPUT: Player message with these fields populated:
      ///  - id
      ///  - properties
      /// OUTPUT: Result message denoting success or failure (and an error if
      /// necessary)
      /// </summary>
      /// <param name="request">The request to send to the server.</param>
      /// <param name="options">The options for the call.</param>
      /// <returns>The response received from the server.</returns>
      public virtual global::Api.CreatePlayerResponse CreatePlayer(global::Api.CreatePlayerRequest request, grpc::CallOptions options)
      {
        return CallInvoker.BlockingUnaryCall(__Method_CreatePlayer, null, options, request);
      }
      /// <summary>
      /// CreatePlayer will put the player  in state storage, and then look
      /// through the 'properties' field for the attributes you have defined as
      /// indices your matchmaker config.  If the attributes exist and are valid
      /// integers, they will be indexed.
      /// INPUT: Player message with these fields populated:
      ///  - id
      ///  - properties
      /// OUTPUT: Result message denoting success or failure (and an error if
      /// necessary)
      /// </summary>
      /// <param name="request">The request to send to the server.</param>
      /// <param name="headers">The initial metadata to send with the call. This parameter is optional.</param>
      /// <param name="deadline">An optional deadline for the call. The call will be cancelled if deadline is hit.</param>
      /// <param name="cancellationToken">An optional token for canceling the call.</param>
      /// <returns>The call object.</returns>
      public virtual grpc::AsyncUnaryCall<global::Api.CreatePlayerResponse> CreatePlayerAsync(global::Api.CreatePlayerRequest request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return CreatePlayerAsync(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      /// <summary>
      /// CreatePlayer will put the player  in state storage, and then look
      /// through the 'properties' field for the attributes you have defined as
      /// indices your matchmaker config.  If the attributes exist and are valid
      /// integers, they will be indexed.
      /// INPUT: Player message with these fields populated:
      ///  - id
      ///  - properties
      /// OUTPUT: Result message denoting success or failure (and an error if
      /// necessary)
      /// </summary>
      /// <param name="request">The request to send to the server.</param>
      /// <param name="options">The options for the call.</param>
      /// <returns>The call object.</returns>
      public virtual grpc::AsyncUnaryCall<global::Api.CreatePlayerResponse> CreatePlayerAsync(global::Api.CreatePlayerRequest request, grpc::CallOptions options)
      {
        return CallInvoker.AsyncUnaryCall(__Method_CreatePlayer, null, options, request);
      }
      /// <summary>
      /// DeletePlayer removes the player from state storage by doing the
      /// following:
      ///  1) Delete player from configured indices.  This effectively removes the
      ///     player from matchmaking when using recommended MMF patterns.
      ///     Everything after this is just cleanup to save stage storage space.
      ///  2) 'Lazily' delete the player's state storage record.  This is kicked
      ///     off in the background and may take some time to complete.
      ///  2) 'Lazily' delete the player's metadata indicies (like, the timestamp when 
      ///     they called CreatePlayer, and the last time the record was accessed).  This 
      ///     is also kicked off in the background and may take some time to complete.
      /// INPUT: Player message with the 'id' field populated.
      /// OUTPUT: Result message denoting success or failure (and an error if
      /// necessary)
      /// </summary>
      /// <param name="request">The request to send to the server.</param>
      /// <param name="headers">The initial metadata to send with the call. This parameter is optional.</param>
      /// <param name="deadline">An optional deadline for the call. The call will be cancelled if deadline is hit.</param>
      /// <param name="cancellationToken">An optional token for canceling the call.</param>
      /// <returns>The response received from the server.</returns>
      public virtual global::Api.DeletePlayerResponse DeletePlayer(global::Api.DeletePlayerRequest request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return DeletePlayer(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      /// <summary>
      /// DeletePlayer removes the player from state storage by doing the
      /// following:
      ///  1) Delete player from configured indices.  This effectively removes the
      ///     player from matchmaking when using recommended MMF patterns.
      ///     Everything after this is just cleanup to save stage storage space.
      ///  2) 'Lazily' delete the player's state storage record.  This is kicked
      ///     off in the background and may take some time to complete.
      ///  2) 'Lazily' delete the player's metadata indicies (like, the timestamp when 
      ///     they called CreatePlayer, and the last time the record was accessed).  This 
      ///     is also kicked off in the background and may take some time to complete.
      /// INPUT: Player message with the 'id' field populated.
      /// OUTPUT: Result message denoting success or failure (and an error if
      /// necessary)
      /// </summary>
      /// <param name="request">The request to send to the server.</param>
      /// <param name="options">The options for the call.</param>
      /// <returns>The response received from the server.</returns>
      public virtual global::Api.DeletePlayerResponse DeletePlayer(global::Api.DeletePlayerRequest request, grpc::CallOptions options)
      {
        return CallInvoker.BlockingUnaryCall(__Method_DeletePlayer, null, options, request);
      }
      /// <summary>
      /// DeletePlayer removes the player from state storage by doing the
      /// following:
      ///  1) Delete player from configured indices.  This effectively removes the
      ///     player from matchmaking when using recommended MMF patterns.
      ///     Everything after this is just cleanup to save stage storage space.
      ///  2) 'Lazily' delete the player's state storage record.  This is kicked
      ///     off in the background and may take some time to complete.
      ///  2) 'Lazily' delete the player's metadata indicies (like, the timestamp when 
      ///     they called CreatePlayer, and the last time the record was accessed).  This 
      ///     is also kicked off in the background and may take some time to complete.
      /// INPUT: Player message with the 'id' field populated.
      /// OUTPUT: Result message denoting success or failure (and an error if
      /// necessary)
      /// </summary>
      /// <param name="request">The request to send to the server.</param>
      /// <param name="headers">The initial metadata to send with the call. This parameter is optional.</param>
      /// <param name="deadline">An optional deadline for the call. The call will be cancelled if deadline is hit.</param>
      /// <param name="cancellationToken">An optional token for canceling the call.</param>
      /// <returns>The call object.</returns>
      public virtual grpc::AsyncUnaryCall<global::Api.DeletePlayerResponse> DeletePlayerAsync(global::Api.DeletePlayerRequest request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return DeletePlayerAsync(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      /// <summary>
      /// DeletePlayer removes the player from state storage by doing the
      /// following:
      ///  1) Delete player from configured indices.  This effectively removes the
      ///     player from matchmaking when using recommended MMF patterns.
      ///     Everything after this is just cleanup to save stage storage space.
      ///  2) 'Lazily' delete the player's state storage record.  This is kicked
      ///     off in the background and may take some time to complete.
      ///  2) 'Lazily' delete the player's metadata indicies (like, the timestamp when 
      ///     they called CreatePlayer, and the last time the record was accessed).  This 
      ///     is also kicked off in the background and may take some time to complete.
      /// INPUT: Player message with the 'id' field populated.
      /// OUTPUT: Result message denoting success or failure (and an error if
      /// necessary)
      /// </summary>
      /// <param name="request">The request to send to the server.</param>
      /// <param name="options">The options for the call.</param>
      /// <returns>The call object.</returns>
      public virtual grpc::AsyncUnaryCall<global::Api.DeletePlayerResponse> DeletePlayerAsync(global::Api.DeletePlayerRequest request, grpc::CallOptions options)
      {
        return CallInvoker.AsyncUnaryCall(__Method_DeletePlayer, null, options, request);
      }
      /// <summary>
      /// GetUpdates streams matchmaking results from Open Match for the
      /// provided player ID.
      /// INPUT: Player message with the 'id' field populated.
      /// OUTPUT: a stream of player objects with one or more of the following
      /// fields populated, if an update to that field is seen in state storage:
      ///  - 'assignment': string that usually contains game server connection information.
      ///  - 'status': string to communicate current matchmaking status to the client.
      ///  - 'error': string to pass along error information to the client.
      ///
      /// During normal operation, the expectation is that the 'assignment' field
      /// will be updated by a Backend process calling the 'CreateAssignments' Backend API
      /// endpoint.  'Status' and 'Error' are free for developers to use as they see fit.  
      /// Even if you had multiple players enter a matchmaking request as a group, the
      /// Backend API 'CreateAssignments' call will write the results to state
      /// storage separately under each player's ID. OM expects you to make all game
      /// clients 'GetUpdates' with their own ID from the Frontend API to get
      /// their results.
      ///
      /// NOTE: This call generates a small amount of load on the Frontend API and state
      ///  storage while watching the player record for updates. You are expected
      ///  to close the stream from your client after receiving your matchmaking
      ///  results (or a reasonable timeout), or you will continue to
      ///  generate load on OM until you do!
      /// NOTE: Just bear in mind that every update will send egress traffic from
      ///  Open Match to game clients! Frugality is recommended.
      /// </summary>
      /// <param name="request">The request to send to the server.</param>
      /// <param name="headers">The initial metadata to send with the call. This parameter is optional.</param>
      /// <param name="deadline">An optional deadline for the call. The call will be cancelled if deadline is hit.</param>
      /// <param name="cancellationToken">An optional token for canceling the call.</param>
      /// <returns>The call object.</returns>
      public virtual grpc::AsyncServerStreamingCall<global::Api.GetUpdatesResponse> GetUpdates(global::Api.GetUpdatesRequest request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return GetUpdates(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      /// <summary>
      /// GetUpdates streams matchmaking results from Open Match for the
      /// provided player ID.
      /// INPUT: Player message with the 'id' field populated.
      /// OUTPUT: a stream of player objects with one or more of the following
      /// fields populated, if an update to that field is seen in state storage:
      ///  - 'assignment': string that usually contains game server connection information.
      ///  - 'status': string to communicate current matchmaking status to the client.
      ///  - 'error': string to pass along error information to the client.
      ///
      /// During normal operation, the expectation is that the 'assignment' field
      /// will be updated by a Backend process calling the 'CreateAssignments' Backend API
      /// endpoint.  'Status' and 'Error' are free for developers to use as they see fit.  
      /// Even if you had multiple players enter a matchmaking request as a group, the
      /// Backend API 'CreateAssignments' call will write the results to state
      /// storage separately under each player's ID. OM expects you to make all game
      /// clients 'GetUpdates' with their own ID from the Frontend API to get
      /// their results.
      ///
      /// NOTE: This call generates a small amount of load on the Frontend API and state
      ///  storage while watching the player record for updates. You are expected
      ///  to close the stream from your client after receiving your matchmaking
      ///  results (or a reasonable timeout), or you will continue to
      ///  generate load on OM until you do!
      /// NOTE: Just bear in mind that every update will send egress traffic from
      ///  Open Match to game clients! Frugality is recommended.
      /// </summary>
      /// <param name="request">The request to send to the server.</param>
      /// <param name="options">The options for the call.</param>
      /// <returns>The call object.</returns>
      public virtual grpc::AsyncServerStreamingCall<global::Api.GetUpdatesResponse> GetUpdates(global::Api.GetUpdatesRequest request, grpc::CallOptions options)
      {
        return CallInvoker.AsyncServerStreamingCall(__Method_GetUpdates, null, options, request);
      }
      /// <summary>Creates a new instance of client from given <c>ClientBaseConfiguration</c>.</summary>
      protected override FrontendClient NewInstance(ClientBaseConfiguration configuration)
      {
        return new FrontendClient(configuration);
      }
    }

    /// <summary>Creates service definition that can be registered with a server</summary>
    /// <param name="serviceImpl">An object implementing the server-side handling logic.</param>
    public static grpc::ServerServiceDefinition BindService(FrontendBase serviceImpl)
    {
      return grpc::ServerServiceDefinition.CreateBuilder()
          .AddMethod(__Method_CreatePlayer, serviceImpl.CreatePlayer)
          .AddMethod(__Method_DeletePlayer, serviceImpl.DeletePlayer)
          .AddMethod(__Method_GetUpdates, serviceImpl.GetUpdates).Build();
    }

    /// <summary>Register service method implementations with a service binder. Useful when customizing the service binding logic.
    /// Note: this method is part of an experimental API that can change or be removed without any prior notice.</summary>
    /// <param name="serviceBinder">Service methods will be bound by calling <c>AddMethod</c> on this object.</param>
    /// <param name="serviceImpl">An object implementing the server-side handling logic.</param>
    public static void BindService(grpc::ServiceBinderBase serviceBinder, FrontendBase serviceImpl)
    {
      serviceBinder.AddMethod(__Method_CreatePlayer, serviceImpl.CreatePlayer);
      serviceBinder.AddMethod(__Method_DeletePlayer, serviceImpl.DeletePlayer);
      serviceBinder.AddMethod(__Method_GetUpdates, serviceImpl.GetUpdates);
    }

  }
}
#endregion
