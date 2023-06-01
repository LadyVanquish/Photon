#pragma once

#if _DEBUG
    #define PH_ASSERT(x, ...) { if(!(x)) { PH_CRITICAL("Assertion Failure: {0}", __VA_ARGS__); __debugbreak(); } }
#else
    #define PH_ASSERT(x, ...)
#endif
