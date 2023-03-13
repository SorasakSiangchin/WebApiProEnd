namespace WebApi.Models.OrderAggregate
{
    public enum OrderStatus
    {
        WaitingForPayment, // กำลังรอการชำระเงิน
        PendingApproval, // รอการอนุมัติ
        SuccessfulPayment // ชำระเงินสำเร็จ
    }
}
