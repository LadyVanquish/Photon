#pragma once

#ifdef _DEBUG

#define PH_CRITICAL(...) Photon::Logger::WindowsLogger->Log(Photon::LogKind::Exception, __VA_ARGS__)
#define PH_ERROR(...)    Photon::Logger::WindowsLogger->Log(Photon::LogKind::Error, __VA_ARGS__)
#define PH_WARNING(...)  Photon::Logger::WindowsLogger->Log(Photon::LogKind::Warning, __VA_ARGS__)
#define PH_MESSAGE(...)  Photon::Logger::WindowsLogger->Log(Photon::LogKind::Message, __VA_ARGS__)
#define PH_INFO(...)     Photon::Logger::WindowsLogger->Log(Photon::LogKind::Information, __VA_ARGS__)
#define PH_NOTE(...)     Photon::Logger::WindowsLogger->Log(Photon::LogKind::Note, __VA_ARGS__)

#else

#define PH_CRITICAL(...)
#define PH_ERROR(...)
#define PH_WARNING(...)
#define PH_MESSAGE(...)
#define PH_INFO(...)
#define PH_NOTE(...)

#endif
