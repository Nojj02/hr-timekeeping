namespace Hr.LeaveApplication

// Employees are able to file
// Managers can approve
// Managers can reject
// Admin can create employees and managers 
// Managers can register themselves as an employee's manager
// Employees can cancel their leaves

type todo = unit
module todo = 
    let func () = ()


type FiledLeaveApplication = todo
type ApprovedLeaveApplication = todo
type RejectedLeaveApplication = todo
type RevisedLeaveApplication = todo
type CancelledLeaveApplication = todo

type UserAction = 
    | Approve
    | Reject
    | Cancel

type Role = 
    | Manager of todo
    | Employee of todo

module LeaveApplication =
    type State = 
        | FiledState of FiledLeaveApplication
        | ApprovedState of ApprovedLeaveApplication
        | RejectedState of RejectedLeaveApplication
        | RevisedState of RevisedLeaveApplication
        | CancelledState of CancelledLeaveApplication

    let transformFromFiledState getAction approve reject cancel (leaveApplication : FiledLeaveApplication) : State =
        match getAction () with
            | Approve -> approve leaveApplication |> ApprovedState
            | Reject -> reject leaveApplication |> RejectedState
            | Cancel -> cancel leaveApplication |> CancelledState
    
    let transformFromApprovedState cancel (leaveApplication : ApprovedLeaveApplication) : State = 
        cancel leaveApplication |> CancelledState
        
    let transformFromRejectedState file (leaveApplication : RejectedLeaveApplication) : State = 
        file leaveApplication |> RevisedState

    let transformFromRevisedState getAction approve reject cancel (leaveApplication : RevisedLeaveApplication) : State = 
        match getAction () with
            | Approve -> approve leaveApplication |> ApprovedState
            | Reject -> reject leaveApplication |> RejectedState
            | Cancel -> cancel leaveApplication |> CancelledState
        
    let transformFromCancelledStatecancel : State = 
           todo.func() |> CancelledState