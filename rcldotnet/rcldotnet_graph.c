// Copyright 2016-2018 Esteve Fernandez <esteve@apache.org>
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

#include <assert.h>
#include <stdlib.h>
#include <stdio.h>

#include <rcl/error_handling.h>
#include <rcl/node.h>
#include <rcl/graph.h>
#include <rcl/rcl.h>
#include <rmw/rmw.h>

#include "rosidl_generator_c/message_type_support_struct.h"

#include "rcldotnet_graph.h"

int native_get_num_topics(void * node_handle)
{
  rcl_names_and_types_t topic_names_and_types = rcl_get_zero_initialized_names_and_types();

  rcl_allocator_t allocator = rcl_get_default_allocator();
  int ret = rcl_get_topic_names_and_types(
    node_handle,
    &allocator,
    1,
    &topic_names_and_types);

  if (ret != RCL_RET_OK) {
    fprintf(stderr, "ERROR====================");
  }

  return topic_names_and_types.names.size;
}

void * native_get_topic_name(void * node_handle, int index)
{
  rcl_names_and_types_t topic_names_and_types = rcl_get_zero_initialized_names_and_types();

  rcl_allocator_t allocator = rcl_get_default_allocator();
  int ret = rcl_get_topic_names_and_types(
    node_handle,
    &allocator,
    1,
    &topic_names_and_types);

  if (ret != RCL_RET_OK) {

    fprintf(stderr, "ERROR====================");
  }

  return topic_names_and_types.names.data[index];
}


int native_get_num_types_topic(void * node_handle, int index)
{
  rcl_names_and_types_t topic_names_and_types = rcl_get_zero_initialized_names_and_types();

  rcl_allocator_t allocator = rcl_get_default_allocator();
  int ret = rcl_get_topic_names_and_types(
    node_handle,
    &allocator,
    1,
    &topic_names_and_types);

  if (ret != RCL_RET_OK) {
    fprintf(stderr, "ERROR====================");
  }

  return topic_names_and_types.types[index].size;
}

void * native_get_type_topic(void * node_handle, int index_topic, int index_type)
{
  rcl_names_and_types_t topic_names_and_types = rcl_get_zero_initialized_names_and_types();

  rcl_allocator_t allocator = rcl_get_default_allocator();
  int ret = rcl_get_topic_names_and_types(
    node_handle,
    &allocator,
    1,
    &topic_names_and_types);

  if (ret != RCL_RET_OK) {
    fprintf(stderr, "ERROR====================");
  }

  return topic_names_and_types.types[index_topic].data[index_type];
}

int native_get_num_services(void * node_handle)
{
  rcl_names_and_types_t service_names_and_types = rcl_get_zero_initialized_names_and_types();

  rcl_allocator_t allocator = rcl_get_default_allocator();
  int ret = rcl_get_service_names_and_types(
    node_handle,
    &allocator,
    &service_names_and_types);

  if (ret != RCL_RET_OK) {
    fprintf(stderr, "ERROR====================");
  }

  return service_names_and_types.names.size;
}

void * native_get_service_name(void * node_handle, int index)
{
  rcl_names_and_types_t service_names_and_types = rcl_get_zero_initialized_names_and_types();

  rcl_allocator_t allocator = rcl_get_default_allocator();
  int ret = rcl_get_service_names_and_types(
    node_handle,
    &allocator,
    &service_names_and_types);

  if (ret != RCL_RET_OK) {

    fprintf(stderr, "ERROR====================");
  }

  return service_names_and_types.names.data[index];
}


int native_get_num_types_service(void * node_handle, int index)
{
  rcl_names_and_types_t service_names_and_types = rcl_get_zero_initialized_names_and_types();

  rcl_allocator_t allocator = rcl_get_default_allocator();
  int ret = rcl_get_service_names_and_types(
    node_handle,
    &allocator,
    &service_names_and_types);

  if (ret != RCL_RET_OK) {
    fprintf(stderr, "ERROR====================");
  }

  return service_names_and_types.types[index].size;
}

void * native_get_type_service(void * node_handle, int index_service, int index_type)
{
  rcl_names_and_types_t service_names_and_types = rcl_get_zero_initialized_names_and_types();

  rcl_allocator_t allocator = rcl_get_default_allocator();
  int ret = rcl_get_service_names_and_types(
    node_handle,
    &allocator,
    &service_names_and_types);

  if (ret != RCL_RET_OK) {
    fprintf(stderr, "ERROR====================");
  }

  return service_names_and_types.types[index_service].data[index_type];
}

int native_get_num_nodes(void * node_handle)
{
  rcutils_string_array_t node_names_c =
    rcutils_get_zero_initialized_string_array();

  rcl_allocator_t allocator = rcl_get_default_allocator();
  int ret = rcl_get_node_names(
    node_handle,
    allocator,
    &node_names_c);

  if (ret != RCL_RET_OK) {
    fprintf(stderr, "ERROR====================");
  }

  return node_names_c.size;
}

void * native_get_node_name(void * node_handle, int index_node)
{
  rcutils_string_array_t node_names_c =
    rcutils_get_zero_initialized_string_array();

  rcl_allocator_t allocator = rcl_get_default_allocator();
  int ret = rcl_get_node_names(
    node_handle,
    allocator,
    &node_names_c);

  if (ret != RCL_RET_OK) {
    fprintf(stderr, "ERROR====================");
  }

  return node_names_c.data[index_node];
}

int native_get_count_publishers(void * node_handle, const char * topic_name)
{
  size_t count;
  size_t ret = rcl_count_publishers(node_handle, topic_name, &count);
  if (ret != RMW_RET_OK) {
    fprintf(stderr, "ERROR====================");
  }
  fprintf(stderr, "Count pub (%s)= %zu", topic_name, count);
  return count;
}

int native_get_count_subscribers(void * node_handle, const char * topic_name)
{
  size_t count;
  size_t ret = rcl_count_subscribers(node_handle, topic_name, &count);
  if (ret != RMW_RET_OK) {
    fprintf(stderr, "ERROR====================");
  }
  fprintf(stderr, "Count sub (%s)= %zu", topic_name, count);
  return count;
}
