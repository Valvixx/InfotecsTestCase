WITH device AS (
    INSERT INTO devices (device_name)
        VALUES (@DeviceName)
        ON CONFLICT (device_name) DO NOTHING
        RETURNING id
),
     device_id AS (
         SELECT id FROM device
         UNION ALL
         SELECT id FROM devices WHERE device_name = @DeviceName
         LIMIT 1
     )
INSERT INTO sessions (device_id, session_name, start_time, end_time, version)
SELECT
    device_id.id,
    @SessionName,
    @StartTime,
    @EndTime,
    @Version
FROM device_id;
