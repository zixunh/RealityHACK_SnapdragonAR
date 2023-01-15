from django_filters import rest_framework as filters
from .models import Mail

class BuildingFilter(filters.FilterSet):
    class Meta:
        model = Mail
        fields = ('id_code', 'name','unread')
