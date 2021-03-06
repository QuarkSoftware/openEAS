﻿//******************************************************************************************************
//  SystemSettings.cs
//
//  Copyright(c) 2016, Electric Power Research Institute, Inc.
//  All rights reserved.
//  
//  Redistribution and use in source and binary forms, with or without
//  modification, are permitted provided that the following conditions are met:
//  
//  * Redistributions of source code must retain the above copyright notice, this
//    list of conditions and the following disclaimer.
//  
//  * Redistributions in binary form must reproduce the above copyright notice,
//    this list of conditions and the following disclaimer in the documentation
//    and/or other materials provided with the distribution.
//  
//  * Neither the name of copyright holder nor the names of its
//    contributors may be used to endorse or promote products derived from
//    this software without specific prior written permission.
//  
//  THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS"
//  AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE
//  IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
//  DISCLAIMED.IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE
//  FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL
//  DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR
//  SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER
//  CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY,
//  OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE
//  OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
//
//  Code Modification History:
//  ----------------------------------------------------------------------------------------------------
//  09/30/2014 - Stephen C. Wills
//       Generated original version of source code.
//
//******************************************************************************************************

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.IO;
using System.Linq;
using GSF;
using GSF.Configuration;

namespace openEAS.Configuration
{
    /// <summary>
    /// Represents the system settings for openXDA.
    /// </summary>
    public class SystemSettings
    {
        #region [ Members ]

        // Fields
        private string m_dbConnectionString;
        private int m_dbTimeout;

        private string m_watchDirectories;
        private string m_resultsPath;
        private string m_filePattern;

        private double m_waitPeriod;
        private double m_timeTolerance;
        private string m_defaultMeterTimeZone;
        private string m_xdaTimeZone;
        private double m_maxTimeOffset;
        private double m_minTimeOffset;
        private double m_maxFileDuration;
        private double m_maxFileCreationTimeOffset;

        private double m_systemFrequency;
        private double m_maxVoltage;
        private double m_maxCurrent;

        private string m_lengthUnits;
        private double m_comtradeMinWaitTime;
        private int m_processingThreadCount;
        private int m_fileWatcherBufferSize;

        private TimeZoneInfo m_defaultMeterTimeZoneInfo;
        private TimeZoneInfo m_xdaTimeZoneInfo;
        private List<string> m_watchDirectoryList;
        private OpenEASSettings m_openEASSettings;
        #endregion

        #region [ Constructors ]

        /// <summary>
        /// Creates a new instance of the <see cref="SystemSettings"/> class.
        /// </summary>
        public SystemSettings()
        {
        }

        /// <summary>
        /// Creates a new instance of the <see cref="SystemSettings"/> class.
        /// </summary>
        /// <param name="connectionString">A string containing the system settings as key-value pairs.</param>
        public SystemSettings(string connectionString)
        {
            ConnectionStringParser<SettingAttribute, CategoryAttribute> parser = new ConnectionStringParser<SettingAttribute, CategoryAttribute>();
            m_openEASSettings = new OpenEASSettings();

            parser.ParseConnectionString(connectionString, this);

        }

        #endregion

        #region [ Properties ]

        /// <summary>
        /// Gets or sets the connection string to the database.
        /// </summary>
        [Setting]
        [DefaultValue("Data Source=localhost; Initial Catalog=openXDA; Integrated Security=SSPI")]
        public string DbConnectionString
        {
            get
            {
                return m_dbConnectionString;
            }
            set
            {
                m_dbConnectionString = value;
            }
        }

        /// <summary>
        /// Gets or sets the amount of time each database
        /// query is given to complete, in seconds.
        /// </summary>
        [Setting]
        [DefaultValue(120)]
        public int DbTimeout
        {
            get
            {
                return m_dbTimeout;
            }
            set
            {
                m_dbTimeout = value;
            }
        }

        /// <summary>
        /// Gets or sets the list of directories to watch for files.
        /// </summary>
        [Setting]
        [DefaultValue("Watch")]
        public string WatchDirectories
        {
            get
            {
                return m_watchDirectories;
            }
            set
            {
                m_watchDirectories = value;

                if ((object)value != null)
                    m_watchDirectoryList = value.Split(Path.PathSeparator).ToList();
            }
        }

        /// <summary>
        /// Gets or sets the path to the fault location
        /// results generated by the fault location engine.
        /// </summary>
        [Setting]
        [DefaultValue("Results")]
        public string ResultsPath
        {
            get
            {
                return m_resultsPath;
            }
            set
            {
                m_resultsPath = value;
            }
        }

        /// <summary>
        /// Gets or sets the pattern used to parse file paths in
        /// order to identify the meter that the file came from.
        /// </summary>
        [Setting]
        [DefaultValue(@"(?<AssetKey>[^\\]+)\\[^\\]+$")]
        public string FilePattern
        {
            get
            {
                return m_filePattern;
            }
            set
            {
                m_filePattern = value;
            }
        }

        /// <summary>
        /// Gets or sets the time zone identifier for the time zone
        /// used by meters in the system unless configured otherwise.
        /// </summary>
        [Setting]
        [DefaultValue("UTC")]
        public string DefaultMeterTimeZone
        {
            get
            {
                return m_defaultMeterTimeZone;
            }
            set
            {
                m_defaultMeterTimeZone = value;
            }
        }

        /// <summary>
        /// Gets or sets the time zone identifier for the
        /// time zone used by openXDA to store data.
        /// </summary>
        /// <remarks>
        /// The default value for this setting (empty string)
        /// causes the setting to assume the value of the local
        /// time zone of the system openXDA is running on.
        /// </remarks>
        [Setting]
        [DefaultValue("")]
        public string XDATimeZone
        {
            get
            {
                return m_xdaTimeZone;
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                    m_xdaTimeZone = TimeZoneInfo.Local.Id;
                else
                    m_xdaTimeZone = value;
            }
        }

        /// <summary>
        /// Gets or sets the amount of time, in seconds,
        /// between the time a file is received and the time
        /// an email should be sent out by the system.
        /// </summary>
        [Setting]
        [DefaultValue(10.0D)]
        public double WaitPeriod
        {
            get
            {
                return m_waitPeriod;
            }
            set
            {
                m_waitPeriod = value;
            }
        }

        /// <summary>
        /// Gets or sets the maximum distance, in seconds,
        /// between a meter's clock and real time.
        /// </summary>
        [Setting]
        [DefaultValue(0.5D)]
        public double TimeTolerance
        {
            get
            {
                return m_timeTolerance;
            }
            set
            {
                m_timeTolerance = value;
            }
        }

        /// <summary>
        /// Gets or sets the maximum number of hours beyond the current system time
        /// before the time of the record indicates that the data is unreasonable.
        /// </summary>
        [Setting]
        [DefaultValue(24.0D)]
        public double MaxTimeOffset
        {
            get
            {
                return m_maxTimeOffset;
            }
            set
            {
                m_maxTimeOffset = value;
            }
        }

        /// <summary>
        /// Gets or sets the maximum number of hours prior to the current system time
        /// before the time of the record indicates that the data is unreasonable.
        /// </summary>
        [Setting]
        [DefaultValue(8760.0D)]
        public double MinTimeOffset
        {
            get
            {
                return m_minTimeOffset;
            }
            set
            {
                m_minTimeOffset = value;
            }
        }

        /// <summary>
        /// Gets or sets the maximum duration, in seconds,
        /// of the files processed by openXDA.
        /// </summary>
        [Setting]
        [DefaultValue(0.0D)]
        public double MaxFileDuration
        {
            get
            {
                return m_maxFileDuration;
            }
            set
            {
                m_maxFileDuration = value;
            }
        }

        /// <summary>
        /// Gets or sets the maximum number of hours prior to the current system time
        /// before the file creation time indicates that the data should not be processed.
        /// </summary>
        [Setting]
        [DefaultValue(0.0D)]
        public double MaxFileCreationTimeOffset
        {
            get
            {
                return m_maxFileCreationTimeOffset;
            }
            set
            {
                m_maxFileCreationTimeOffset = value;
            }
        }

        /// <summary>
        /// Gets or sets the system frequency.
        /// </summary>
        [Setting]
        [DefaultValue(60.0D)]
        public double SystemFrequency
        {
            get
            {
                return m_systemFrequency;
            }
            set
            {
                m_systemFrequency = value;
            }
        }

        /// <summary>
        /// Gets or sets the per-unit threshold at which the
        /// voltage exceeds engineering reasonableness.
        /// </summary>
        [Setting]
        [DefaultValue(2.0D)]
        public double MaxVoltage
        {
            get
            {
                return m_maxVoltage;
            }
            set
            {
                m_maxVoltage = value;
            }
        }

        /// <summary>
        /// Gets or sets the threshold, in amps, at which the
        /// current exceeds engineering reasonableness.
        /// </summary>
        [Setting]
        [DefaultValue(1000000.0D)]
        public double MaxCurrent
        {
            get
            {
                return m_maxCurrent;
            }
            set
            {
                m_maxCurrent = value;
            }
        }

        /// <summary>
        /// Gets or sets the units of measure to use
        /// for lengths (line length and fault distance).
        /// </summary>
        [Setting]
        [DefaultValue("miles")]
        public string LengthUnits
        {
            get
            {
                return m_lengthUnits;
            }
            set
            {
                m_lengthUnits = value;
            }
        }

        /// <summary>
        /// Gets or sets the minimum amount of time to wait for additional data
        /// files after the system detects the existence of a .D00 COMTRADE file.
        /// </summary>
        [Setting]
        [DefaultValue(15.0D)]
        public double COMTRADEMinWaitTime
        {
            get
            {
                return m_comtradeMinWaitTime;
            }
            set
            {
                m_comtradeMinWaitTime = value;
            }
        }

        /// <summary>
        /// Gets or sets the number of threads used
        /// for processing meter data concurrently.
        /// </summary>
        /// <remarks>
        /// Values less than or equal to zero will be set to the number of logical processors.
        /// </remarks>
        [Setting]
        [DefaultValue(0)]
        public int ProcessingThreadCount
        {
            get
            {
                return m_processingThreadCount;
            }
            set
            {
                m_processingThreadCount = value;

                if (m_processingThreadCount <= 0)
                    m_processingThreadCount = Environment.ProcessorCount;
            }
        }

        /// <summary>
        /// Gets or sets the size of the
        /// <see cref="FileSystemWatcher"/>s' internal buffers.
        /// </summary>
        /// <seealso cref="FileSystemWatcher.InternalBufferSize"/>
        [Setting]
        [DefaultValue(8192)]
        public int FileWatcherBufferSize
        {
            get
            {
                return m_fileWatcherBufferSize;
            }
            set
            {
                m_fileWatcherBufferSize = value;
            }
        }

        /// <summary>
        /// Gets the <see cref="TimeZoneInfo"/> for the time zone
        /// used by meters in the system unless configured otherwise.
        /// </summary>
        public TimeZoneInfo DefaultMeterTimeZoneInfo
        {
            get
            {
                if ((object)m_defaultMeterTimeZoneInfo == null)
                    m_defaultMeterTimeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById(m_defaultMeterTimeZone);

                return m_defaultMeterTimeZoneInfo;
            }
        }

        /// <summary>
        /// Gets the <see cref="TimeZoneInfo"/> for the
        /// time zone used by openXDA to store data.
        /// </summary>
        public TimeZoneInfo XDATimeZoneInfo
        {
            get
            {
                if ((object)m_xdaTimeZoneInfo == null)
                    m_xdaTimeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById(m_xdaTimeZone);

                return m_xdaTimeZoneInfo;
            }
        }

        /// <summary>
        /// Gets a list of directories to be watched
        /// for files containing fault records.
        /// </summary>
        public IReadOnlyCollection<string> WatchDirectoryList
        {
            get
            {
                return m_watchDirectoryList.AsReadOnly();
            }
        }


        [Category]
        [SettingName("OpenEAS")]
        public OpenEASSettings OpenEASSettings
        {
            get
            {
                return m_openEASSettings;
            }
        }

        #endregion

        #region [ Methods ]

        public string ToConnectionString()
        {
            ConnectionStringParser<SettingAttribute> parser = new ConnectionStringParser<SettingAttribute>();
            parser.ExplicitlySpecifyDefaults = true;
            return parser.ComposeConnectionString(this);
        }

        #endregion

        #region [ Static ]

        // Static Fields
        private static readonly SystemSettings DefaultSystemSettings = new SystemSettings(string.Empty);
        private static readonly string DefaultConnectionString = DefaultSystemSettings.ToConnectionString();

        // Static Methods
        public static string ToConnectionString(Dictionary<string, string> settings)
        {
            List<CategorizedSetting> categorizedSettings = settings
                .Select(kvp => new CategorizedSetting(kvp.Key, kvp.Value))
                .ToList();

            string categorizedConnectionString = ToConnectionString(categorizedSettings, 0);

            return Merge(categorizedConnectionString, DefaultConnectionString);
        }

        private static string ToConnectionString(List<CategorizedSetting> settings, int level)
        {
            IEnumerable<string> connectionStrings;

            if (settings.Count == 1)
            {
                if (level < settings[0].Categories.Count)
                    return string.Format("{0}={{{1}}}", settings[0].Categories[level], ToConnectionString(settings, level + 1));

                return settings[0].Value;
            }

            connectionStrings = settings
                .Where(setting => level < setting.Categories.Count)
                .GroupBy(setting => setting.Categories[level])
                .Select(grouping => string.Format("{0}={{{1}}}", grouping.Key, ToConnectionString(grouping.ToList(), level + 1)));

            return string.Join(";", connectionStrings);
        }

        private static string Merge(string primaryConnectionString, string connectionString)
        {
            Dictionary<string, string> primarySettings = primaryConnectionString.ParseKeyValuePairs();
            Dictionary<string, string> settings = connectionString.ParseKeyValuePairs();

            foreach (KeyValuePair<string, string> kvp in primarySettings)
                settings[kvp.Key] = kvp.Value;

            return settings.JoinKeyValuePairs();
        }

        #endregion
    }
}
