using System;

public class CEnum {

	public enum EGameType : int {
        None = 0,
        Player = 1,

        BG = 100,
        BG_1 = 101,
        BG_2 = 102,

        Layer_1 = 200,
        Layer_1a = 201,
        Layer_1b = 202,
        Layer_1c = 203,
        Layer_1d = 204,

        Layer_2 = 300,
        Layer_2a = 301,
        Layer_2b = 302,
        Layer_2c = 303,
        Layer_2d = 304,

        Layer_3 = 400,
        Layer_3a = 401,
        Layer_3b = 402,
        Layer_3c = 403,
        Layer_3d = 404,

        FG = 600,
        FG_1 = 601,
        FG_2 = 602,
        FG_3 = 603,
        FG_4 = 604,
        FG_5 = 605,
        FG_6 = 606,
        FG_7 = 607,
        FG_8 = 608,
        FG_9 = 609,
        FG_10 = 610,

        Ground = 1000,
        Ground_1 = 1001,
        Ground_2 = 1002,
        Ground_3 = 1003,
        Ground_4 = 1004,
        Ground_5 = 1005,
        Ground_6 = 1006,
        Ground_7 = 1007,
        Ground_8 = 1008,
        Ground_9 = 1009,
        Ground_10 = 1010,

        Trap = 2000,
        Gear = 2001
    }

    public enum EPlayerState : int {
        None = 0,
        Idle = 1,
        Run = 2,
        Fail = 10
    }

}
