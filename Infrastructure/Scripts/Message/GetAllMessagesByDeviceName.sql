SELECT
    d.device_name AS "DeviceName",
    s.session_name AS "SessionName",
    s.start_time AS "StartTime",
    s.end_time AS "EndTime",
    s.version AS "Version"
FROM sessions s
JOIN devices d ON s.device_id=d.id
WHERE d.device_name = @DeviceName;
