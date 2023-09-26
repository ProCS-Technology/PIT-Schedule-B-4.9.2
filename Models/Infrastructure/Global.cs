namespace ProcsDLL.Models.Infrastructure
{
    public class Global
    {
        public Global()
        {
        }
        public enum TaskType
        {
            All,
            Any
        }
        public enum TaskStatus
        {
            Assigned,
            Pending,
            Approved,
            Rejected,
            Compliant,
            PartiallyCompliant
        }
        public enum Module
        {
            UserMaster,
            AccessControl,
            Password,
            MeetingCreation,
            CircularResolution,
            DraftMinutes,
            FinalMinutes,
            ActionItem,
            Notice,
            AnnotationSharing,
            Evaluation,
            SharedFolder,
            MeetingAction,
            Calendar,
            Attendance
        }
        public enum Activity
        {
            AdditionOfDirectorOrCS,
            UserActivationOrDeactivation,
            AnyChangeInUserAccess,
            ForgotPassword,
            ResetPassword,
            AgendaUploadOrPublish,
            AdditionOrDeletionOfItem,
            EditOrUpdatesInItem,
            PublishingOfItem,
            WithdrawalOfItem,
            PublishingOfCircularResolution,
            AccentOrDecentOrAbstain,
            ConfigurableNotificationIfDirectorDoesNotVoteWithinSpecifiedTime,
            OnAchieving2by3rdMajority,
            PublishingOfDraftMinutes,
            PublishingOfFinalMinutes,
            AllocationOfActionItem,
            SendingMeetingNotice,
            SharingOfDrawingAndHighlightsAndComments,
            ReplyOrChat,
            PublicationOfEvaluation,
            SubmittingEvaluation,
            SharingOfDocuments,
            RescheduleOrCancellationOfMeeting,
            BlockingOfCalendar,
            MarkingOfAttendance
        }
    }
}