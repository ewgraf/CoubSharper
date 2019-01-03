using System;
using System.Collections.Generic;

namespace CoubSharper {
	public enum OrderBy { // lower_dash-case for Uris
		likes_count,
		views_count,
		newest,
		oldest,
		newest_popular
	}

	public class AccessModes {
		public string LoggedIn = "logged_in"; // the default access mode
		public string Create = "create"; // with the ability to create coub videos
		public string Like = "like"; // with the ability to do a like on coub videos
		public string Recoub = "recoub"; // with the ability to recoub coub videos
		public string Follow = "follow"; // with the ability to follow channels
		public string ChannelEdit = "channel_edit"; // with the ability to edit channels
	}

	public class CoubToken
	{
		public string access_token { get; set; }
		public string token_type { get; set; }
		public int? expires_in { get; set; }
		public string scope { get; set; }
		public int? created_at { get; set; }
	}

	public class Med
	{
		public string url { get; set; }
		public int? size { get; set; }
	}

	public class High
	{
		public string url { get; set; }
		public int? size { get; set; }
	}

	public class Video
	{
		public Med med { get; set; }
		public High high { get; set; }
	}

	public class High2
	{
		public string url { get; set; }
		public int? size { get; set; }
	}

	public class Med2
	{
		public string url { get; set; }
		public int? size { get; set; }
	}

	public class Audio
	{
		public High2 high { get; set; }
		public Med2 med { get; set; }
		public double? sample_duration { get; set; }
	}

	public class Html5
	{
		public Video video { get; set; }
		public Audio audio { get; set; }
	}

	public class Mobile
	{
		public string gifv { get; set; }
		public string[] audio { get; set; }
	}

	public class FileVersions
	{
		public Html5 html5 { get; set; }
		public Mobile mobile { get; set; }
	}

	public class Chunks
	{
		public string template { get; set; }
		public string[] versions { get; set; }
		public int[] chunks { get; set; }
	}

	public class AudioVersions
	{
		public string template { get; set; }
		public string[] versions { get; set; }
		public Chunks chunks { get; set; }
	}

	public class ImageVersions
	{
		public string template { get; set; }
		public string[] versions { get; set; }
	}

	public class FirstFrameVersions
	{
		public string template { get; set; }
		public string[] versions { get; set; }
	}

	public class Dimensions
	{
		public int[] big { get; set; }
		public int[] med { get; set; }
	}

	public class ExternalDownload
	{
		public string type { get; set; }
		public string service_name { get; set; }
		public string url { get; set; }
		public bool? has_embed { get; set; }
	}

	public class AvatarVersions
	{
		public string template { get; set; }
		public string[] versions { get; set; }
	}

	public class Channel
	{
		public int? id { get; set; }
		public string permalink { get; set; }
		public string title { get; set; }
		public object description { get; set; }
		public int? followers_count { get; set; }
		public int? following_count { get; set; }
		public AvatarVersions avatar_versions { get; set; }
	}

	public class Meta
	{
		public string service { get; set; }
		public string duration { get; set; }
	}

	public class ExternalRawVideo
	{
		public int? id { get; set; }
		public string title { get; set; }
		public string url { get; set; }
		public string image { get; set; }
		public string image_retina { get; set; }
		public Meta meta { get; set; }
		public double? duration { get; set; }
		public int? raw_video_id { get; set; }
		public bool? has_embed { get; set; }
	}

	public class Meta2
	{
		public string service { get; set; }
		public string duration { get; set; }
	}

	public class ExternalVideo
	{
		public int? id { get; set; }
		public string title { get; set; }
		public string url { get; set; }
		public string image { get; set; }
		public string image_retina { get; set; }
		public Meta2 meta { get; set; }
		public double? duration { get; set; }
		public int? raw_video_id { get; set; }
		public bool? has_embed { get; set; }
	}

	public class MediaBlocks
	{
		public object[] uploaded_raw_videos { get; set; }
		public ExternalRawVideo[] external_raw_videos { get; set; }
		public object[] remixed_from_coubs { get; set; }
		public ExternalVideo external_video { get; set; }
	}

	public class EditorialInfo
	{
	}

	public class Tags
	{
		public int? id { get; set; }
		public string title { get; set; }
		public string value { get; set; }
	}

	public class Category
	{
		public string big_image_url { get; set; }
		public int? id { get; set; }
		public string med_image_url { get; set; }
		public string permalink { get; set; }
		public string small_image_url { get; set; }
		public int? subscriptions_count { get; set; }
		public string title { get; set; }
		public bool? visible { get; set; }
	}

	public class Coub
	{
		public object flag { get; set; }
		public object abuses { get; set; }
		public object recoubs_by_users_channels { get; set; }
		public bool? favourite { get; set; }
		public object recoub { get; set; }
		public object like { get; set; }
		public bool? in_my_best2015 { get; set; }
		public int? id { get; set; }
		public string type { get; set; }
		public string permalink { get; set; }
		public string title { get; set; }
		public string visibility_type { get; set; }
		public string original_visibility_type { get; set; }
		public int? channel_id { get; set; }
		public DateTime created_at { get; set; }
		public DateTime updated_at { get; set; }
		public bool? is_done { get; set; }
		public int? views_count { get; set; }
		public object cotd { get; set; }
		public object cotd_at { get; set; }
		public bool? published { get; set; }
		public DateTime published_at { get; set; }
		public bool? reversed { get; set; }
		public bool? from_editor_v2 { get; set; }
		public bool? is_editable { get; set; }
		public bool? original_sound { get; set; }
		public bool? has_sound { get; set; }
		public object recoub_to { get; set; }
		public FileVersions file_versions { get; set; }
		public AudioVersions audio_versions { get; set; }
		public ImageVersions image_versions { get; set; }
		public FirstFrameVersions first_frame_versions { get; set; }
		public Dimensions dimensions { get; set; }
		public int[] site_w_h { get; set; }
		public int[] page_w_h { get; set; }
		public int[] site_w_h_small { get; set; }
		public bool? age_restricted { get; set; }
		public bool? age_restricted_by_admin { get; set; }
		public bool? not_safe_for_work { get; set; }
		public bool? allow_reuse { get; set; }
		public bool? dont_crop { get; set; }
		public bool? banned { get; set; }
		public bool? global_safe { get; set; }
		public string audio_file_url { get; set; }
		public object external_download { get; set; }
		public object application { get; set; }
		public Channel channel { get; set; }
		public object file { get; set; }
		public string picture { get; set; }
		public string timeline_picture { get; set; }
		public string small_picture { get; set; }
		public object sharing_picture { get; set; }
		public int? percent_done { get; set; }
		public Tags[] tags { get; set; }
		public Category[] categories { get; set; }
		public int? recoubs_count { get; set; }
		public int? remixes_count { get; set; }
		public int? likes_count { get; set; }
		public int? raw_video_id { get; set; }
		public bool? uploaded_by_ios_app { get; set; }
		public bool? uploaded_by_android_app { get; set; }
		public MediaBlocks media_blocks { get; set; }
		public string raw_video_thumbnail_url { get; set; }
		public string raw_video_title { get; set; }
		public bool? video_block_banned { get; set; }
		public double? duration { get; set; }
		public bool? promo_winner { get; set; }
		public object promo_winner_recoubers { get; set; }
		public EditorialInfo editorial_info { get; set; }
		public object promo_hint { get; set; }
		public object beeline_best_2014 { get; set; }
		public bool? from_web_editor { get; set; }
		public bool? normalize_sound { get; set; }
		public bool? best2015_addable { get; set; }
		public object ahmad_promo { get; set; }
		public object promo_data { get; set; }
		public object audio_copyright_claim { get; set; }
		public bool? ads_disabled { get; set; }
		public bool? is_safe_for_ads { get; set; }
	}

	public class CoubsSearchResponse
	{
		public string page { get; set; }
		public int? total_pages { get; set; }
		public int? per_page { get; set; }
		public Coub[] coubs { get; set; }
	}
}
