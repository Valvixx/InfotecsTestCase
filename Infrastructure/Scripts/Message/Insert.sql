WITH new_device AS (
    INSERT INTO devices (name)
        VALUES (@deviceName)
        ON CONFLICT (name) DO NOTHING
        RETURNING id
)
INSERT INTO sessions (device_id, name, start_time)
SELECT id, @sessionName, @startTime
FROM new_device;