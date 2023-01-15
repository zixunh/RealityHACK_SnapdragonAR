import random
import string
import uuid

from django.db import models


# def ZeroPosition():
#     return {
#         'x':  0,
#         'y':  0,
#         'z':  0,
#         'rx': 0,
#         'ry': 0,
#         'rz': 0,
#     }

# Create your models here.
class Mail(models.Model):
    id_code = models.CharField(verbose_name='mail', primary_key=True, max_length = 50, default=uuid.uuid4, editable=False, auto_created=True)
    name = models.CharField(max_length=128, default = 'Untitled')
    content = models.CharField(max_length=128, default = 'Under Review')
    unread = models.BooleanField(default = True)

    created_at = models.DateTimeField(auto_now_add=True,null=True)
    updated_at = models.DateTimeField(auto_now=True,null=True)
    def __str__(self):
        return self.name