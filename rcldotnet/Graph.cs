/* Copyright 2016-2018 Esteve Fernandez <esteve@apache.org>
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

using ROS2.Common;
using ROS2.Interfaces;
using ROS2.Utils;

namespace ROS2 {
  internal class GraphDelegates {
    private static readonly DllLoadUtils dllLoadUtils;

    // For GetTopicNamesAndTypes
    [UnmanagedFunctionPointer (CallingConvention.Cdecl)]
    internal delegate int NativeGetNumTopicsType (IntPtr nodeHandle);

    internal static NativeGetNumTopicsType native_get_num_topics = null;

    [UnmanagedFunctionPointer (CallingConvention.Cdecl)]
    internal delegate int NativeGetNumTypesTopicType (IntPtr nodeHandle, int index_topic);

    internal static NativeGetNumTypesTopicType native_get_num_types_topic = null;

    [UnmanagedFunctionPointer (CallingConvention.Cdecl)]
    internal delegate IntPtr NativeGetTopicNameType (IntPtr nodeHandle, int index);

    internal static NativeGetTopicNameType native_get_topic_name = null;

    [UnmanagedFunctionPointer (CallingConvention.Cdecl)]
    internal delegate IntPtr NativeGetTypeTopicType (IntPtr nodeHandle, int index_topic, int index_type);

    internal static NativeGetTypeTopicType native_get_type_topic = null;

    // For GetServiceNamesAndTypes
    [UnmanagedFunctionPointer (CallingConvention.Cdecl)]
    internal delegate int NativeGetNumServicesType (IntPtr nodeHandle);

    internal static NativeGetNumServicesType native_get_num_services = null;

    [UnmanagedFunctionPointer (CallingConvention.Cdecl)]
    internal delegate int NativeGetNumTypesServiceType (IntPtr nodeHandle, int index_service);

    internal static NativeGetNumTypesServiceType native_get_num_types_service = null;

    [UnmanagedFunctionPointer (CallingConvention.Cdecl)]
    internal delegate IntPtr NativeGetServiceNameType (IntPtr nodeHandle, int index);

    internal static NativeGetServiceNameType native_get_service_name = null;

    [UnmanagedFunctionPointer (CallingConvention.Cdecl)]
    internal delegate IntPtr NativeGetTypeServiceType (IntPtr nodeHandle, int index_service, int index_type);

    internal static NativeGetTypeServiceType native_get_type_service = null;

    // For GetNodeNames
    [UnmanagedFunctionPointer (CallingConvention.Cdecl)]
    internal delegate int NativeGetNumNodesType (IntPtr nodeHandle);

    internal static NativeGetNumNodesType native_get_num_nodes = null;

    [UnmanagedFunctionPointer (CallingConvention.Cdecl)]
    internal delegate IntPtr NativeGetNodeNameType (IntPtr nodeHandle, int index);

    internal static NativeGetNodeNameType native_get_node_name = null;

    // For CountPublishers and CountSubscribers
    [UnmanagedFunctionPointer (CallingConvention.Cdecl)]
    internal delegate int NativeGetCountPublishersType (IntPtr nodeHandle,
      [MarshalAs (UnmanagedType.LPStr)] string topic_name);

    internal static NativeGetCountPublishersType native_get_count_publishers = null;

    [UnmanagedFunctionPointer (CallingConvention.Cdecl)]
    internal delegate int NativeGetCountSubscribersType (IntPtr nodeHandle,
      [MarshalAs (UnmanagedType.LPStr)] string topic_name);

    internal static NativeGetCountSubscribersType native_get_count_subscribers = null;

    static GraphDelegates () {
      dllLoadUtils = DllLoadUtilsFactory.GetDllLoadUtils ();
      IntPtr nativelibrary = dllLoadUtils.LoadLibrary ("rcldotnet_graph");

      // For GetTopicNamesAndTypes
      IntPtr native_get_num_topics_ptr = dllLoadUtils.GetProcAddress (
        nativelibrary, "native_get_num_topics");
      GraphDelegates.native_get_num_topics =
        (NativeGetNumTopicsType) Marshal.GetDelegateForFunctionPointer (
          native_get_num_topics_ptr, typeof (NativeGetNumTopicsType));

      IntPtr native_get_num_types_topic_ptr = dllLoadUtils.GetProcAddress (
        nativelibrary, "native_get_num_types_topic");
      GraphDelegates.native_get_num_types_topic =
        (NativeGetNumTypesTopicType) Marshal.GetDelegateForFunctionPointer (
          native_get_num_types_topic_ptr, typeof (NativeGetNumTypesTopicType));

      IntPtr native_get_topic_name_ptr = dllLoadUtils.GetProcAddress (
        nativelibrary, "native_get_topic_name");
      GraphDelegates.native_get_topic_name =
        (NativeGetTopicNameType) Marshal.GetDelegateForFunctionPointer (
          native_get_topic_name_ptr, typeof (NativeGetTopicNameType));

      IntPtr native_get_type_topic_ptr = dllLoadUtils.GetProcAddress (
        nativelibrary, "native_get_type_topic");
      GraphDelegates.native_get_type_topic =
        (NativeGetTypeTopicType) Marshal.GetDelegateForFunctionPointer (
          native_get_type_topic_ptr, typeof (NativeGetTypeTopicType));

      // For GetServiceNamesAndTypes
      IntPtr native_get_num_services_ptr = dllLoadUtils.GetProcAddress (
        nativelibrary, "native_get_num_services");
      GraphDelegates.native_get_num_services =
        (NativeGetNumServicesType) Marshal.GetDelegateForFunctionPointer (
          native_get_num_services_ptr, typeof (NativeGetNumServicesType));

      IntPtr native_get_num_types_service_ptr = dllLoadUtils.GetProcAddress (
        nativelibrary, "native_get_num_types_service");
      GraphDelegates.native_get_num_types_service =
        (NativeGetNumTypesServiceType) Marshal.GetDelegateForFunctionPointer (
          native_get_num_types_service_ptr, typeof (NativeGetNumTypesServiceType));

      IntPtr native_get_service_name_ptr = dllLoadUtils.GetProcAddress (
        nativelibrary, "native_get_service_name");
      GraphDelegates.native_get_service_name =
        (NativeGetServiceNameType) Marshal.GetDelegateForFunctionPointer (
          native_get_service_name_ptr, typeof (NativeGetServiceNameType));

      IntPtr native_get_type_service_ptr = dllLoadUtils.GetProcAddress (
        nativelibrary, "native_get_type_service");
      GraphDelegates.native_get_type_service =
        (NativeGetTypeServiceType) Marshal.GetDelegateForFunctionPointer (
          native_get_type_service_ptr, typeof (NativeGetTypeServiceType));

      // For GetNodeNames
      IntPtr native_get_num_nodes_ptr = dllLoadUtils.GetProcAddress (
        nativelibrary, "native_get_num_nodes");
      GraphDelegates.native_get_num_nodes =
        (NativeGetNumNodesType) Marshal.GetDelegateForFunctionPointer (
          native_get_num_nodes_ptr, typeof (NativeGetNumNodesType));

      IntPtr native_get_node_name_ptr = dllLoadUtils.GetProcAddress (
        nativelibrary, "native_get_node_name");
      GraphDelegates.native_get_node_name =
        (NativeGetNodeNameType) Marshal.GetDelegateForFunctionPointer (
          native_get_node_name_ptr, typeof (NativeGetNodeNameType));

      // For CountPublishers
      IntPtr native_get_count_publishers_ptr = dllLoadUtils.GetProcAddress (
        nativelibrary, "native_get_count_publishers");
      GraphDelegates.native_get_count_publishers =
        (NativeGetCountPublishersType) Marshal.GetDelegateForFunctionPointer (
          native_get_count_publishers_ptr, typeof (NativeGetCountPublishersType));

      IntPtr native_get_count_subscribers_ptr = dllLoadUtils.GetProcAddress (
        nativelibrary, "native_get_count_subscribers");
      GraphDelegates.native_get_count_subscribers =
        (NativeGetCountSubscribersType) Marshal.GetDelegateForFunctionPointer (
          native_get_count_subscribers_ptr, typeof (NativeGetCountSubscribersType));
    }
  }

  public class Graph : IGraph {

    Node node_base_;

    public Graph (Node node_base) {
      node_base_ = node_base;
    }

    public SortedDictionary< String, List<String> > GetTopicNamesAndTypes(bool noDemangle = false)
    {
      SortedDictionary< String, List<String> > ret = new SortedDictionary< String, List<String> >();

      int count_topics = GraphDelegates.native_get_num_topics(node_base_.Handle);

      for (int i = 0; i < count_topics; i++)
      {
        System.String topic_id = Marshal.PtrToStringAnsi( GraphDelegates.native_get_topic_name(node_base_.Handle, i) );

        int count_types = GraphDelegates.native_get_num_types_topic(node_base_.Handle, i);
        List<String> types = new List<String>();

        for (int j = 0; j < count_types; j++)
        {
          System.String type_id = Marshal.PtrToStringAnsi( GraphDelegates.native_get_type_topic(node_base_.Handle, i, j) );
          types.Add(type_id);
        }
        ret[topic_id] = types;
      }

      return ret;
    }

    public SortedDictionary< String, List<String> > GetServiceNamesAndTypes()
    {
      SortedDictionary< String, List<String> > ret = new SortedDictionary< String, List<String> >();

      int count_services = GraphDelegates.native_get_num_services(node_base_.Handle);

      for (int i = 0; i < count_services; i++)
      {
        System.String topic_id = Marshal.PtrToStringAnsi( GraphDelegates.native_get_service_name(node_base_.Handle, i) );

        int count_types = GraphDelegates.native_get_num_types_service(node_base_.Handle, i);
        List<String> types = new List<String>();

        for (int j = 0; j < count_types; j++)
        {
          System.String type_id = Marshal.PtrToStringAnsi( GraphDelegates.native_get_type_service(node_base_.Handle, i, j) );
          types.Add(type_id);
        }
        ret[topic_id] = types;
      }

      return ret;
    }

    public List< String > GetNodeNames()
    {
      List< String > ret = new List< String >();

      int count_nodes = GraphDelegates.native_get_num_nodes(node_base_.Handle);

      for (int i = 0; i < count_nodes; i++)
      {
        System.String node_id = Marshal.PtrToStringAnsi( GraphDelegates.native_get_node_name(node_base_.Handle, i) );
        ret.Add(node_id);
      }

      return ret;
    }


    public int CountPublishers(string topicName)
    {
      return GraphDelegates.native_get_count_publishers(node_base_.Handle, topicName);
    }

    public int CountSubscribers(string topicName)
    {
      return GraphDelegates.native_get_count_subscribers(node_base_.Handle, topicName);
    }

    public void NotifyShutdown()
    {

    }
  }
}
