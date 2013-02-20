#ifdef __cplusplus
extern "C" {
#endif

#include <stdarg.h>
#include <stddef.h>
#include <stdint.h>

// Tear up/down
extern void init(int size);
extern void shutdown();

// Common
extern void remove(int index);
extern int get_size(int index);

// Synchronous marshal-all-the-bytes methods
extern void put(int index, const char* data, int size);
extern void get(int index, char* output, int offset, int amount);

#ifdef __cplusplus
}
#endif
