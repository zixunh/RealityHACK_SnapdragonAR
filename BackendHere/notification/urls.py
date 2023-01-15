from django.urls import include, path
from rest_framework import routers
from .views import NotificationBoard, NewEmail, unreadMailInfo, checkNewMail

# BASE_URL = 'http://localhost:8888/notification/'; Method = POST
urlpatterns = [
    #OpenNotificationBoard
    path("", NotificationBoard.as_view(), name="notify"), 

    #SendAMessage
    path("mail", NewEmail.as_view(), name="email"), 

    #ReadUnreadMail
    path("unread", unreadMailInfo),

    #CheckIfNewMail
    path("check", checkNewMail)
]


############################
###### OpenNotificationBoard
'''
body
{
    "deep": true/false, 
    # if true, open message itself as well
}
no return
'''

###### SendAMessage
'''
body
{
    "subject": "Reality",
    "message": "HACK!!!"
}
return the Message being sent
'''

###### ReadUnreadMail
'''
body
{
    "readmode": "noread" / "readone" / "readall", 
    # if readall, set all mails with unread=False
    # if readone, set the latest unread mail with unread=False
    # if noread, leave all unread mails with unread=True
}
whatever return 
    1. count of unread mails after read operation
    2. content of all the unread mails existing
    3. content of the latest unread mail
'''

###### CheckIfNewMail and CountUnreadMails
'''
empty body
return 1. if here is a new mail 2. count of remaining unread mails
'''

