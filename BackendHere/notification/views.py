# Create your views here.
from django.db import transaction
from django.http import HttpResponse, JsonResponse
from django.shortcuts import render
from rest_framework import (generics, mixins, permissions, status, views,
                            viewsets)
from rest_framework.response import Response
from django.core.mail import send_mail
from django.conf import settings
from .models import Mail
from rest_framework.exceptions import APIException
import json
from .serializers import serialize_mail
import os
import time
from django.http import QueryDict


# Create your views here.
def openNotification():
    os.system('cliclick c:1430,20')
    time.sleep(0.1)
    os.system('cliclick c:1300,100')
    os.system('cliclick m:0,0')
    print('openNotification!')

def openNotification_deep():
    os.system('cliclick c:1430,20')
    time.sleep(0.1)
    os.system('cliclick c:1300,100')
    time.sleep(0.1)
    os.system('cliclick c:1300,100')
    os.system('cliclick m:0,0')
    print('openNotification!Deep!')

def mailSend(subject='A cool subject',
             message='A stunning message'): # postman-->backend-->database
    send_mail(
        subject=subject,
        message=message,
        from_email=settings.EMAIL_HOST_USER,
        recipient_list=[settings.RECIPIENT_ADDRESS])
    print('mailSend!')

# 
def unreadMailInfo(request): # return num of unread mails; content of these mails;
    unreads = Mail.objects.filter(unread=True).order_by('-created_at')
    unread_count = unreads.count()

    #unread modify
    try:
        readmode = json.loads(request.body).get('readmode', 'noread')
    except:
        readmode = request.data.get('readmode', 'noread')
        
    # readmode = request.data.get('readmode', 'noread')
    print(readmode)
    try:
        if readmode=='readall': #readmode=='readone'/'readall'/'noread'
            # print('readall')
            unread_count = 0
            with transaction.atomic():
                for item in unreads:
                    item.unread = False
                    item.save()
                    print(item.unread)
        elif readmode=='readone':
            unread_count = max(unread_count - 1,0)
            with transaction.atomic():
                item = unreads[0]
                item.unread = False
                item.save()
                print(item.unread)
    except:
        pass

    # serialization
    latest_mail = serialize_mail(unreads[0])
    all_unread_mails = []
    for item in unreads:
        all_unread_mails.append(serialize_mail(item))

    return JsonResponse({
        'status': status.HTTP_200_OK,
        'unread_count': unread_count,
        'unread_preview': all_unread_mails,
        'latest_preview': latest_mail
    })


def checkNewMail(request): # return whether here is a new mail or not #for loop
    mails = Mail.objects.order_by('-created_at')
    unread_count = mails.filter(unread=True).count()

    return JsonResponse({
        'has_new': mails[0].unread,
        'unread_count': unread_count
        })


class NotificationBoard(views.APIView): # open notification board
    def post(self, request, format=None):
        print(request.data)
        if request.data.get('deep', False):
            openNotification_deep()
        else: 
            openNotification()
        return JsonResponse({'status': status.HTTP_200_OK})

class NewEmail(views.APIView): # post a new mail
    # def get(self)
    def post(self, request, format=None):
        # send email
        mail_content = request.data
        print(mail_content)
        
        # mail_content = json.loads(json.dumps(mail_content))
        # print(mail_content.keys())


        subject = mail_content.get('subject','Reality')
        message = mail_content.get('message','Hack!!! snap drag!!')
        mailSend(subject=subject, message=message)
        # build database
        # try:
        with transaction.atomic():
            mail_item = Mail(
                name = subject,
                content = message,
                unread = True,
            )
            mail_item.save()
            print("model creating!")
        # except Exception as e:
        #     raise APIException(str(e))
        return JsonResponse({
            'status': status.HTTP_200_OK,
            'id': mail_item.id_code,
            'subject': mail_item.name,
            'message': mail_item.content,
            'unread': mail_item.unread,
        })

# class UnreadViewSet(
#     mixins.RetrieveModelMixin,
#     # mixins.ListModelMixin,
#     viewsets.GenericViewSet,
#     mixins.UpdateModelMixin,
#     mixins.CreateModelMixin):
#     queryset = Mail.objects.all()
#     serializer_class = MailSerializer
#     filter_class = MailFilter
#     permission_classes = [permissions.AllowAny]

#     def post(self, request, *args, **kwargs):
#         readmode = request.data['category']
#         try:
#             with transaction.atomic():

# class MailViewSet(
#     mixins.RetrieveModelMixin,
#     # mixins.ListModelMixin,
#     viewsets.GenericViewSet,
#     mixins.UpdateModelMixin,
#     # mixins.DestroyModelMixin,
#     mixins.CreateModelMixin):
#     queryset = Mail.objects.all()
#     serializer_class = MailSerializer
#     filter_class = MailFilter
#     permission_classes = [permissions.AllowAny]

#     def create(self, request, *args, **kwargs):
#         category = request.data['category']
#         try:
#             with transaction.atomic():
#                 if category == 'ZPlus':
#                     with open('media/MailInstanceCases/002instacelzt.json',encoding='UTF-8') as f:
#                         instance = json.load(f)
#                     request.data['instance'] = instance
#                     with open('media/StaticMeshCases/002UIlzt.json',encoding='UTF-8') as f:
#                         static_mesh = json.load(f)
#                     request.data['static_mesh'] = static_mesh

#                 serializer = self.get_serializer(data=request.data) 
#                 serializer.is_valid(raise_exception=True)
#                 # print(serializer.data)
#                 self.perform_create(serializer)
#                 headers = self.get_success_headers(serializer.data)
#                 # print(serializer)
#                 return Response(serializer.data, status=status.HTTP_201_CREATED, headers=headers)
#         except Exception as e:
#             raise APIException(str(e))