/////////////////////////////////////////////////////////////////////////////////////////////////////
//
// Audiokinetic Wwise generated include file. Do not edit.
//
/////////////////////////////////////////////////////////////////////////////////////////////////////

#ifndef __WWISE_IDS_H__
#define __WWISE_IDS_H__

#include <AK/SoundEngine/Common/AkTypes.h>

namespace AK
{
    namespace EVENTS
    {
        static const AkUniqueID BACK = 1559875400U;
        static const AkUniqueID CLOSEBOX = 24651142U;
        static const AkUniqueID CLOSEDOOR = 1405443347U;
        static const AkUniqueID COMBAT_START = 1137411260U;
        static const AkUniqueID DARK_DARKER = 2450551025U;
        static const AkUniqueID DARK_LIGHT = 1358872724U;
        static const AkUniqueID GAMEOVER = 4158285989U;
        static const AkUniqueID ICECREAMCOLLECT = 4190943754U;
        static const AkUniqueID ICECREAMPROX = 3183478735U;
        static const AkUniqueID JUMP = 3833651337U;
        static const AkUniqueID LIGHT_DARK = 3330710522U;
        static const AkUniqueID LIKING = 1730364821U;
        static const AkUniqueID MUSIC = 3991942870U;
        static const AkUniqueID OPENBOX = 1847332492U;
        static const AkUniqueID OPENDOOR = 2122995345U;
        static const AkUniqueID OPTION = 731737678U;
        static const AkUniqueID POTIONCOLLECT = 989237998U;
        static const AkUniqueID POTIONPROX = 1273320011U;
        static const AkUniqueID SELECT = 1432588725U;
        static const AkUniqueID STEPS = 1718617278U;
        static const AkUniqueID UNICORNATTACK = 3839500963U;
        static const AkUniqueID VEGGIEATTACK = 2190275314U;
        static const AkUniqueID VEGGIEPROX = 1142507785U;
        static const AkUniqueID VICTORY = 2716678721U;
    } // namespace EVENTS

    namespace STATES
    {
        namespace MUSIC_STATE
        {
            static const AkUniqueID GROUP = 3826569560U;

            namespace STATE
            {
                static const AkUniqueID COMBAT_DARK = 457771164U;
                static const AkUniqueID GAMEPLAY_DARK = 1723393528U;
                static const AkUniqueID GAMEPLAY_LIGHT = 4255815960U;
                static const AkUniqueID NONE = 748895195U;
                static const AkUniqueID VICTORY = 2716678721U;
            } // namespace STATE
        } // namespace MUSIC_STATE

        namespace PLAYERLIFE
        {
            static const AkUniqueID GROUP = 444815956U;

            namespace STATE
            {
                static const AkUniqueID ALIVE = 655265632U;
                static const AkUniqueID DEAD = 2044049779U;
                static const AkUniqueID NONE = 748895195U;
            } // namespace STATE
        } // namespace PLAYERLIFE

        namespace WORLD
        {
            static const AkUniqueID GROUP = 2609808943U;

            namespace STATE
            {
                static const AkUniqueID DARK = 1925914845U;
                static const AkUniqueID LIGHT = 1935470627U;
                static const AkUniqueID NONE = 748895195U;
            } // namespace STATE
        } // namespace WORLD

    } // namespace STATES

    namespace SWITCHES
    {
        namespace GAMEPLAY_SWITCH
        {
            static const AkUniqueID GROUP = 2702523344U;

            namespace SWITCH
            {
                static const AkUniqueID COMBAT = 2764240573U;
                static const AkUniqueID EXPLORE = 579523862U;
            } // namespace SWITCH
        } // namespace GAMEPLAY_SWITCH

    } // namespace SWITCHES

    namespace GAME_PARAMETERS
    {
        static const AkUniqueID CLOCKTICKING = 1051411130U;
        static const AkUniqueID SS_AIR_FEAR = 1351367891U;
        static const AkUniqueID SS_AIR_FREEFALL = 3002758120U;
        static const AkUniqueID SS_AIR_FURY = 1029930033U;
        static const AkUniqueID SS_AIR_MONTH = 2648548617U;
        static const AkUniqueID SS_AIR_PRESENCE = 3847924954U;
        static const AkUniqueID SS_AIR_RPM = 822163944U;
        static const AkUniqueID SS_AIR_SIZE = 3074696722U;
        static const AkUniqueID SS_AIR_STORM = 3715662592U;
        static const AkUniqueID SS_AIR_TIMEOFDAY = 3203397129U;
        static const AkUniqueID SS_AIR_TURBULENCE = 4160247818U;
    } // namespace GAME_PARAMETERS

    namespace BANKS
    {
        static const AkUniqueID INIT = 1355168291U;
        static const AkUniqueID MAIN = 3161908922U;
    } // namespace BANKS

    namespace BUSSES
    {
        static const AkUniqueID ENEMIES = 2242381963U;
        static const AkUniqueID ENVIRONMENT = 1229948536U;
        static const AkUniqueID ITEMS = 2151963051U;
        static const AkUniqueID MASTER_AUDIO_BUS = 3803692087U;
        static const AkUniqueID MENU = 2607556080U;
        static const AkUniqueID MUSIC = 3991942870U;
        static const AkUniqueID PLAYER = 1069431850U;
        static const AkUniqueID SFX = 393239870U;
    } // namespace BUSSES

    namespace AUX_BUSSES
    {
        static const AkUniqueID DARK_WORLD_REV = 1662838314U;
    } // namespace AUX_BUSSES

    namespace AUDIO_DEVICES
    {
        static const AkUniqueID NO_OUTPUT = 2317455096U;
        static const AkUniqueID SYSTEM = 3859886410U;
    } // namespace AUDIO_DEVICES

}// namespace AK

#endif // __WWISE_IDS_H__
