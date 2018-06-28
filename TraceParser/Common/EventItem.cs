using System;
using TraceParser.Eutra;
using TraceParser.S1ap;
using TraceParser.X2ap;
using HandoverCommand = TraceParser.S1ap.HandoverCommand;
using HandoverRequest = TraceParser.S1ap.HandoverRequest;
using HandoverRequestAcknowledge = TraceParser.S1ap.HandoverRequestAcknowledge;

namespace TraceParser.Common
{
    public class EventHead
    {
        public DateTime EventDay { get; set; }

        public int EventHour { get; set; }

        public string EventId { get; set; }

        public DateTime EventTime { get; set; }

        public string EventType { get; set; }

        public string GlobalCellId { get; set; }

        public string Imsi { get; set; }

        public string MmeUeS1apid { get; set; }

        public string RacUeRef { get; set; }
    }

    public class EventObj
    {
        public string Cause { get; set; }

        public string CauseValue { get; set; }

        public string cRnti { get; set; }

        public string dl_CarrierFreq { get; set; }

        public string e_rab_id { get; set; }

        public string EnbS1apId { get; set; }

        public string EventChildId { get; set; }

        public string EventId { get; set; }

        public string EventType { get; set; }

        public string GlobalCellId { get; set; }

        public string Gummei { get; set; }

        public string Guti { get; set; }

        public string handoverType { get; set; }

        public int Hour { get; set; }

        public string Imsi { get; set; }

        public string InnerEventId { get; set; }

        public string MCC { get; set; }

        public string MeasId { get; set; }

        public int Millisec { get; set; }

        public int Minute { get; set; }

        public string Mmec { get; set; }

        public string MmeS1apId { get; set; }

        public string MNC { get; set; }

        public string mTmsi { get; set; }

        public string NeiPhysCellId { get; set; }

        public string NeiRsrpResult { get; set; }

        public string NeiRsrqResult { get; set; }

        public string NewUEIdentity { get; set; }

        public string RacUeRef { get; set; }

        public string radioNetwork { get; set; }

        public string rat_type { get; set; }

        public string ReleaseCause { get; set; }

        public string ReportConfigId { get; set; }

        public string RRC_Establishment_Cause { get; set; }

        public string RsrpResult { get; set; }

        public string RsrqResult { get; set; }

        public int Second { get; set; }

        public string servingPLMNId { get; set; }

        public string SourceUEIdentity { get; set; }

        public string tAC { get; set; }

        public string targetCell_ID { get; set; }

        public string TargetECI { get; set; }

        public string TargetPhysCellId { get; set; }

        public string TargetpLMNId { get; set; }

        public string transmissionMode { get; set; }

        public string UE_X2AP_ID { get; set; }

        public string UEIdentityIndexValue { get; set; }
    }

    public class EventUECapabilityInformation
    {
        public EventHead eventhead { get; set; }

        public UECapabilityInformation UECapabilityInformation { get; set; }
    }

    public class EventUserIdentify
    {
        public string GlobalCellId { get; set; }

        public string MmeS1apId { get; set; }

        public string RacUeRef { get; set; }

        public DateTime TraceTime { get; set; }
    }

    public class EventMeasurementReport
    {
        public EventHead eventhead { get; set; }

        public MeasurementReport MeasurementReport { get; set; }
    }

    public class EventRRCConnectionReconfiguration
    {
        public EventHead eventhead { get; set; }

        public RRCConnectionReconfiguration RRCConnectionReconfiguration { get; set; }
    }

    public class EventRRCConnectionReconfigurationComplete
    {
        public EventHead eventhead { get; set; }

        public RRCConnectionReconfigurationComplete RRCConnectionReconfigurationComplete { get; set; }
    }

    public class EventRRCConnectionReject
    {
        public EventHead eventhead { get; set; }

        public RRCConnectionReject RRCConnectionReject { get; set; }
    }

    public class EventRRCConnectionRelease
    {
        public EventHead eventhead { get; set; }

        public RRCConnectionRelease RRCConnectionRelease { get; set; }
    }

    public class EventRRCConnectionRequest
    {
        public EventHead eventhead { get; set; }

        public RRCConnectionRequest RRCConnectionRequest { get; set; }
    }

    public class EventRRCConnectionSetup
    {
        public EventHead eventhead { get; set; }

        public RRCConnectionSetup RRCConnectionSetup { get; set; }
    }

    public class EventRRCConnectionSetupComplete
    {
        public EventHead eventhead { get; set; }

        public RRCConnectionSetupComplete RRCConnectionSetupComplete { get; set; }
    }

    public class EventS1HandoverCommand
    {
        public EventHead eventhead { get; set; }

        public HandoverCommand HandoverCommand { get; set; }
    }

    public class EventS1HandoverRequest
    {
        public EventHead eventhead { get; set; }

        public HandoverRequest HandoverRequest { get; set; }
    }

    public class EventS1HandoverRequestAcknowledge
    {
        public EventHead eventhead { get; set; }

        public HandoverRequestAcknowledge HandoverRequestAcknowledge { get; set; }
    }

    public class EventS1HandoverRequired
    {
        public EventHead eventhead { get; set; }

        public HandoverRequired HandoverRequired { get; set; }
    }

    public class EventS1InitialContextSetupRequest
    {
        public EventHead eventhead { get; set; }

        public InitialContextSetupRequest InitialContextSetupRequest { get; set; }
    }

    public class EventS1InitialUEMessage
    {
        public EventHead eventhead { get; set; }

        public InitialUEMessage InitialUEMessage { get; set; }
    }

    public class EventS1UEContextReleaseCommand
    {
        public EventHead eventhead { get; set; }

        public UEContextReleaseCommand UEContextReleaseCommand { get; set; }
    }

    public class EventX2HandoverRequest
    {
        public EventHead eventhead { get; set; }

        public X2ap.HandoverRequest HandoverRequest { get; set; }
    }

    public class EventX2HandoverRequestAcknowledge
    {
        public EventHead eventhead { get; set; }

        public X2ap.HandoverRequestAcknowledge HandoverRequestAcknowledge { get; set; }
    }

    public class EventX2UEContextRelease
    {
        public EventHead eventhead { get; set; }

        public UEContextRelease UEContextRelease { get; set; }
    }

}
