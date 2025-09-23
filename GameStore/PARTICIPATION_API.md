# Participation Controller API Documentation

The ParticipationController manages the many-to-many relationship between Drivers and Grand Prix events. It handles driver assignments to races and provides various ways to query participation data.

## Base URL
```
http://localhost:5251/api/participation
```

---

## 📋 **Endpoints Overview**

| Method | Endpoint | Description |
|--------|----------|-------------|
| POST | `/api/participation` | Assign a driver to a Grand Prix |
| GET | `/api/participation/{id}` | Get participation by ID |
| GET | `/api/participation/grandprix/{gpId}` | Get all drivers in a Grand Prix |
| GET | `/api/participation/driver/{driverId}` | Get all Grand Prix for a driver |

---

## 🎯 **1. Assign Driver to Grand Prix**

**`POST /api/participation`**

Assigns a driver to participate in a specific Grand Prix event.

### Request
```json
POST /api/participation
Content-Type: application/json

{
    "driverId": 2,
    "grandPrixId": 1
}
```

### Request Body Schema
```typescript
{
    driverId: number;    // ID of the driver to assign
    grandPrixId: number; // ID of the Grand Prix event
}
```

### Success Response (201 Created)
```json
{
    "id": 1,
    "driverId": 2,
    "driver": {
        "id": 2,
        "driverNumber": 44,
        "firstName": "Lewis",
        "lastName": "Hamilton",
        "acronym": "HAM",
        "teamName": "Mercedes",
        "participations": []
    },
    "grandPrixId": 1,
    "grandPrix": {
        "id": 1,
        "name": "Monaco Grand Prix",
        "location": "Monaco",
        "laps": 78,
        "length": 3.337,
        "participations": []
    }
}
```

### Error Responses

**409 Conflict - Driver Already Assigned**
```json
"Driver already assigned to this Grand Prix."
```

**400 Bad Request - Invalid Data**
```json
{
    "type": "https://tools.ietf.org/html/rfc7231#section-6.5.1",
    "title": "One or more validation errors occurred.",
    "status": 400,
    "errors": {
        "DriverId": ["The DriverId field is required."],
        "GrandPrixId": ["The GrandPrixId field is required."]
    }
}
```

---

## 🔍 **2. Get Participation by ID**

**`GET /api/participation/{id}`**

Retrieves a specific participation record with complete driver and Grand Prix details.

### Request
```
GET /api/participation/1
```

### Success Response (200 OK)
```json
{
    "id": 1,
    "driverId": 2,
    "driver": {
        "id": 2,
        "driverNumber": 44,
        "firstName": "Lewis",
        "lastName": "Hamilton",
        "acronym": "HAM",
        "teamName": "Mercedes",
        "participations": []
    },
    "grandPrixId": 1,
    "grandPrix": {
        "id": 1,
        "name": "Monaco Grand Prix",
        "location": "Monaco",
        "laps": 78,
        "length": 3.337,
        "participations": []
    }
}
```

### Error Response (404 Not Found)
```json
{
    "type": "https://tools.ietf.org/html/rfc7231#section-6.5.4",
    "title": "Not Found",
    "status": 404
}
```

---

## 🏎️ **3. Get Drivers in a Grand Prix**

**`GET /api/participation/grandprix/{gpId}`**

Returns all drivers participating in a specific Grand Prix event.

### Request
```
GET /api/participation/grandprix/1
```

### Success Response (200 OK)
```json
[
    {
        "id": 2,
        "driverNumber": 44,
        "firstName": "Lewis",
        "lastName": "Hamilton",
        "acronym": "HAM",
        "teamName": "Mercedes",
        "participations": []
    },
    {
        "id": 3,
        "driverNumber": 1,
        "firstName": "Max",
        "lastName": "Verstappen",
        "acronym": "VER",
        "teamName": "Red Bull Racing",
        "participations": []
    }
]
```

### Empty Response (200 OK)
```json
[]
```

---

## 🏆 **4. Get Grand Prix for a Driver**

**`GET /api/participation/driver/{driverId}`**

Returns all Grand Prix events where a specific driver is participating.

### Request
```
GET /api/participation/driver/2
```

### Success Response (200 OK)
```json
[
    {
        "id": 1,
        "name": "Monaco Grand Prix",
        "location": "Monaco",
        "laps": 78,
        "length": 3.337,
        "participations": []
    },
    {
        "id": 2,
        "name": "Spanish Grand Prix",
        "location": "Barcelona",
        "laps": 66,
        "length": 4.675,
        "participations": []
    }
]
```

### Empty Response (200 OK)
```json
[]
```

---

## 📊 **Data Models**

### Participation
```typescript
{
    id: number;
    driverId: number;
    driver: Driver;
    grandPrixId: number;
    grandPrix: GrandPrix;
}
```

### Driver
```typescript
{
    id: number;
    driverNumber: number;
    firstName: string;
    lastName: string;
    acronym: string;
    teamName: string | null;
    participations: Participation[];
}
```

### GrandPrix
```typescript
{
    id: number;
    name: string;
    location: string | null;
    laps: number | null;
    length: number | null;
    participations: Participation[];
}
```

### CreateParticipationDto
```typescript
{
    driverId: number;
    grandPrixId: number;
}
```

---

## 🔧 **Business Rules**

1. **Unique Participation**: A driver can only be assigned to the same Grand Prix once
2. **Database Constraint**: Composite unique index on (DriverId, GrandPrixId)
3. **Referential Integrity**: Both driver and Grand Prix must exist before creating participation
4. **Cascade Behavior**: Deleting a driver or Grand Prix may affect related participations

---

## 📝 **Usage Examples**

### Scenario: Assign Lewis Hamilton to Monaco GP
```bash
# 1. Create participation
curl -X POST http://localhost:5251/api/participation \
  -H "Content-Type: application/json" \
  -d '{
    "driverId": 2,
    "grandPrixId": 1
  }'

# 2. Get all drivers in Monaco GP
curl http://localhost:5251/api/participation/grandprix/1

# 3. Get all races for Lewis Hamilton
curl http://localhost:5251/api/participation/driver/2
```

### Scenario: Check Participation Details
```bash
# Get specific participation record
curl http://localhost:5251/api/participation/1
```

---

## ⚠️ **Common Error Scenarios**

1. **Duplicate Assignment**: Trying to assign the same driver to the same Grand Prix twice
2. **Invalid IDs**: Using non-existent driver or Grand Prix IDs
3. **Missing Data**: Not providing required driverId or grandPrixId
4. **Database Constraint**: Violating the unique composite index

---

## 🚀 **Integration Notes**

- **CORS Enabled**: React frontend on `http://localhost:5173` is allowed
- **JSON Format**: Clean JSON without reference preservation markers
- **Async Operations**: All database operations are asynchronous
- **Error Handling**: Proper HTTP status codes and descriptive error messages
- **Related Data**: All responses include related driver and Grand Prix information where applicable

This API follows RESTful principles and provides a complete interface for managing driver-race assignments in a Formula 1 management system.