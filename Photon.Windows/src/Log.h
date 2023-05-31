#pragma once

#ifdef _DEBUG

#define PH_CRITICAL(...) Photon::Logger::WindowsLogger->Log(Photon::LogEventType::Exception, __VA_ARGS__)
#define PH_ERROR(...)    Photon::Logger::WindowsLogger->Log(Photon::LogEventType::Error, __VA_ARGS__)
#define PH_WARNING(...)  Photon::Logger::WindowsLogger->Log(Photon::LogEventType::Warning, __VA_ARGS__)
#define PH_MESSAGE(...)  Photon::Logger::WindowsLogger->Log(Photon::LogEventType::Message, __VA_ARGS__)
#define PH_INFO(...)     Photon::Logger::WindowsLogger->Log(Photon::LogEventType::Information, __VA_ARGS__)
#define PH_NOTE(...)     Photon::Logger::WindowsLogger->Log(Photon::LogEventType::Note, __VA_ARGS__)

#else

#define PH_CRITICAL(...)
#define PH_ERROR(...)
#define PH_WARNING(...)
#define PH_MESSAGE(...)
#define PH_INFO(...)
#define PH_NOTE(...)

#endif
