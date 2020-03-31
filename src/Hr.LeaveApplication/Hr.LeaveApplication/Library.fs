namespace Hr.LeaveApplication

// Employees are able to file

// Managers can approve
// Managers can reject
// Admin can create employees and managers 
// Managers can register themselves as an employee's manager
// Employees can cancel their leaves

type Reason = string

type ReasonForRejection = Reason

type Employee =
    {
        EmployeeNumber: string;
    }

type Manager = Employee

type DatesAppliedFor = 
    {
        Start: System.DateTime;
        End: System.DateTime;
    }

module LeaveApplicationModule =

    type FiledLeaveApplication = 
        {
            Employee: Employee;
            DatesAppliedFor: DatesAppliedFor;
            Reason: Reason;
        }

    type ApprovedLeaveApplication =
        FiledLeaveApplication * Manager
        
    type RejectedLeaveApplication =
        FiledLeaveApplication * Manager * ReasonForRejection
    
    type RevisedLeaveApplication =
        RejectedLeaveApplication
        
    type LeaveApplication =
        | FiledLeaveApplication of FiledLeaveApplication
        | ApprovedLeaveApplication of ApprovedLeaveApplication
        | RejectedLeaveApplication of RejectedLeaveApplication
        | RevisedLeaveApplication of RevisedLeaveApplication

    let fileLeave (employee: Employee) (datesAppliedFor: DatesAppliedFor) (reason: Reason) : FiledLeaveApplication =
        {
            Employee = employee;
            DatesAppliedFor = datesAppliedFor;
            Reason = reason;
        }

    let approveLeaveApplication (manager: Manager) (leaveApplication: LeaveApplication) : ApprovedLeaveApplication =
        match leaveApplication with
            | FiledLeaveApplication filed -> filed, manager
            | RevisedLeaveApplication revised -> revised, manager
            | ApprovedLeaveApplication approved -> Error
            | RejectedLeaveApplication rejected -> Error
            | _ -> Error
        
    let rejectLeaveApplication (manager: Manager) (leaveApplication: FiledLeaveApplication) (reason: ReasonForRejection) : RejectedLeaveApplication =
        leaveApplication, manager, reason


    // Test
    let employee = { EmployeeNumber = "001" }
    let date = 
        { 
            Start = new System.DateTime(2000,1,1);
            End = new System.DateTime(2000,1,2);
        }
    let reason = "annual leave" : Reason
    let leave = fileLeave employee date reason

    let manager = { EmployeeNumber = "guy" } : Manager
    let rejectedLeave = rejectLeaveApplication manager leave "not a good reason"

    let refiledLeave = reviseLeave rejectedLeave rejectedLeave