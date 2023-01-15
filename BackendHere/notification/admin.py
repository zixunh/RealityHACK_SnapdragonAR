from django.contrib import admin
from django.contrib.auth.admin import UserAdmin
from .models import Mail
# Register your models here.
# admin.site.register(Building)
# admin.site.register(BuildingComponent)
@admin.register(Mail)
class MailAdmin(admin.ModelAdmin):
    list_display = ['id_code', 'name','content','created_at']