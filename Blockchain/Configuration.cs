using System;
using System.Collections.Generic;
using System.Text;

namespace Blockchain
{
    public class Configuration
    {
        private static int NULL_NUMBER_VALUE = -1;
        private static DateTime NULL_DATETIME_VALUE = DateTime.MinValue;
        private static int BLOCK_GENERATION_INTERVAL = 10;
        private static int DIFFICULTY_ADJUSTMENT_INTERVAL = 10;
        private static int BLOCK_UTCTIME_MINUTES_OFFSET = 1;

        internal static Settings CurrentSettings = new Settings
        {
            NullNumberValue = NULL_NUMBER_VALUE,
            NullDatetimeValue = NULL_DATETIME_VALUE,
            BlockGenerationInterval = BLOCK_GENERATION_INTERVAL,
            DifficultyAdjustmentInterval = DIFFICULTY_ADJUSTMENT_INTERVAL,
            BlockUTCTimeMinutesOffsetMinus = BLOCK_UTCTIME_MINUTES_OFFSET,
            BlockUTCTimeMinutesOffsetPlus = BLOCK_UTCTIME_MINUTES_OFFSET
        };

        public class Settings
        {
            public int NullNumberValue { get; set; }
            public DateTime NullDatetimeValue { get; set; }
            public int BlockGenerationInterval { get; set; }
            public int DifficultyAdjustmentInterval { get; set; }
            public int BlockUTCTimeMinutesOffsetMinus { get; set; }
            public int BlockUTCTimeMinutesOffsetPlus { get; set; }
        }

        public static void SetSettings(Settings settings)
        {
            CurrentSettings = settings;
        }

        public static Settings GetSettings()
        {
            return CurrentSettings;
        }
    }
}
