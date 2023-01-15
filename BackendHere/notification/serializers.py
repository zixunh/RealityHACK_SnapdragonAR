from rest_framework import serializers
from .models import Mail

class MailSerializer(serializers.ModelSerializer):
    class Meta:
        model = Mail
        read_only_field = ("id_code",)
        field = '_all_'

def serialize_mail(mail):
    return {
        'subtitle': mail.name,
        'message': mail.content,
        'unread': mail.unread
    }