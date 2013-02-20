#include "c.h"
#include <stdlib.h>
#include <unistd.h>
#include <string.h>

char** _data;
int* _sizes;
int _size;

extern "C" {

  // Tear up/down
  void init(int size) {
    _size = size;
    _data = new char*[size];
    _sizes = new int[size];
    for(int i =0; i < _size;  i++)
      _data[i] = NULL;
  }
  void shutdown() {
    for(int i =0; i < _size;  i++)
      if(_data[i])
        delete[] _data[i];
    delete [] _sizes;
    delete[] _data;
  }

  // Common
  void remove(int index) {
    delete[] _data[index];
    _sizes[index] = 0;
  }
  int get_size(int index) {
    return _sizes[index];
  }

  // Syncrhonous, marshal-all-the-bytes methods
  void put(int index, const char* data, int size) {
    _data[index] = new char[size];
    _sizes[index] = size;
    memcpy(_data[index], data, size);
  }

  void get(int index, char* output, int offset, int amount) {
    memcpy(output, _data[index] + offset, amount);
  }
}

